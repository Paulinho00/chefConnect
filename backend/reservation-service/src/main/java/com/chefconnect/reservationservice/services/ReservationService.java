package com.chefconnect.reservationservice.services;

import java.time.Instant;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.UUID;
import java.util.stream.Collectors;

import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.oauth2.jwt.Jwt;

import org.slf4j.LoggerFactory;
import org.slf4j.Logger;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Service;

import com.chefconnect.reservationservice.domain.Reservation;
import com.chefconnect.reservationservice.domain.ReservationStatus;
import com.chefconnect.reservationservice.domain.TableReservation;
import com.chefconnect.reservationservice.exceptions.ReservationNotFoundException;
import com.chefconnect.reservationservice.repository.ReservationRepository;
import com.chefconnect.reservationservice.repository.TableReservationRepository;
import com.chefconnect.reservationservice.services.Dto.MessageResponseDto;
import com.chefconnect.reservationservice.services.Dto.ReservationDto;
import com.chefconnect.reservationservice.services.Dto.RestaurantServicesDto.RestaurantDto;
import com.chefconnect.reservationservice.services.Dto.SqsDto.ReservationSqsDto;
import com.chefconnect.reservationservice.services.Dto.SqsDto.RestaurantSqsDto;

import io.awspring.cloud.sqs.operations.SqsTemplate;
import lombok.extern.slf4j.Slf4j;

@Slf4j
@Service
public class ReservationService {

    @Value("${events.queues.event-queue}")
    private String queueUrl;

    private SqsTemplate sqsTemplate;
    private ReservationRepository reservationRepository;
    private TableReservationRepository tableReservationRepository;
    private RestaurantService restaurantService;

    private static final Logger logger = LoggerFactory.getLogger(ReservationService.class);

    public ReservationService(
        ReservationRepository reservationRepository, 
        TableReservationRepository tableReservationRepository,
        RestaurantService restaurantService,
        SqsTemplate sqsTemplate){
        this.reservationRepository = reservationRepository;
        this.tableReservationRepository = tableReservationRepository;
        this.restaurantService = restaurantService;
        this.sqsTemplate = sqsTemplate;
    }

    public Reservation createReservation(UUID restaurantId, String date, int numberOfPeople) {
        try {
            LocalDateTime reservationDate = LocalDateTime.parse(date);

            // User user = userRepository.findById(userId)
            //     .orElseThrow(() -> new IllegalArgumentException("Użytkownik o podanym ID nie istnieje."));

            Reservation reservation = new Reservation();
            reservation.setRestaurantId(restaurantId);
            reservation.setDate(reservationDate);
            reservation.setNumberOfPeople(numberOfPeople);
            reservation.setUserId(this.getUserId());
            reservation.setReservationStatus(ReservationStatus.UNCONFIRMED);

            reservation.setApproved(false);
            reservation.setDeleted(false);

            // SQS
            this.sendToQueue(reservation, restaurantId, date);
            
            return reservationRepository.save(reservation);

        } catch (Exception e) {
            throw new IllegalArgumentException("Błąd przy tworzeniu rezerwacji: " + e.getMessage());
        }
    }

    private void sendToQueue(Reservation reservation, UUID restaurantId, String reservationDate){
        RestaurantDto restaurant = restaurantService.getRestaurant(restaurantId);

        ReservationSqsDto reservationSqsDto = new ReservationSqsDto();
        reservationSqsDto.setDate(reservationDate + "Z");

        RestaurantSqsDto restaurantSqsDto = new RestaurantSqsDto();
        restaurantSqsDto.setId(restaurant.getId());
        restaurantSqsDto.setName(restaurant.getName());
        restaurantSqsDto.setAddress(restaurant.getAddress());
        reservationSqsDto.setRestaurant(restaurantSqsDto);

        reservationSqsDto.setNumberOfPeople(reservation.getNumberOfPeople());
        
        try {
            sqsTemplate.send(queueUrl, reservationSqsDto);
        }
        catch (Exception e) {
            logger.error("Nieoczekiwany błąd: {}", e.getMessage(), e);
            throw new RuntimeException("Nieoczekiwany błąd przy wysyłaniu wiadomości", e);
        }
    }

    public List<ReservationDto> getUserReservations() {
        UUID userId = this.getUserId();

        List<Reservation> reservations = reservationRepository.findByUserIdAndIsDeletedFalse(userId);

        return reservations.stream()
                .map(reservation -> convertToDto(reservation))
                .collect(Collectors.toList());
    }

    public MessageResponseDto cancelReservation(UUID reservationId) {
        Reservation reservation = reservationRepository.findById(reservationId)
                .orElseThrow(() -> new ReservationNotFoundException(reservationId));
        
        reservation.setReservationStatus(ReservationStatus.CANCELLED);
        
        reservationRepository.save(reservation);
        
        return new MessageResponseDto("Pomyślnie anulowano");
    }

    public void confirmReservation(UUID reservationId, List<UUID> tableIds) {
        Reservation reservation = reservationRepository.findById(reservationId)
                .orElseThrow(() -> new RuntimeException("Rezerwacja nie została znaleziona"));

        reservation.setReservationStatus(ReservationStatus.CONFIRMED);
        reservation.setApprovingWorkerId(getUserId());
        reservation.setApproved(true);

        List<TableReservation> tables = tableReservationRepository.findAllById(tableIds);
        Set<TableReservation> tablesSet = new HashSet<>(tables);
        reservation.setTableReservations(tablesSet);

        reservationRepository.save(reservation);
    }

    public List<Reservation> getAllUnconfirmedReservationsForRestaurant(UUID restaurantId) {
        return reservationRepository.findByReservationStatusAndRestaurantId(ReservationStatus.UNCONFIRMED, restaurantId);
    }

    private UUID getUserId(){
        // Pobieramy użytkownika z Cognito
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        Jwt jwt = (Jwt) authentication.getPrincipal();
        String userIdString = jwt.getClaimAsString("sub");
        return UUID.fromString(userIdString);
    }

    private ReservationDto convertToDto(Reservation reservation) {
        return new ReservationDto(
            reservation.getId(),
            restaurantService.getRestaurant(reservation.getRestaurantId()).getAddress(),
            reservation.getTableReservations().size(),
            reservation.getDate(),
            reservation.getReservationStatus()
        );
    }
}
