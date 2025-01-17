package com.chefconnect.reservationservice.services;

import java.time.Instant;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
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
import com.chefconnect.reservationservice.exceptions.ReservationNotFoundException;
import com.chefconnect.reservationservice.repository.ReservationRepository;
import com.chefconnect.reservationservice.repository.TableReservationRepository;
import com.chefconnect.reservationservice.services.Dto.AvailableTablesResponseDto;
import com.chefconnect.reservationservice.services.Dto.MessageResponseDto;
import com.chefconnect.reservationservice.services.Dto.ReservationDto;
import com.chefconnect.reservationservice.services.Dto.RestaurantServicesDto.RestaurantDto;
import com.chefconnect.reservationservice.services.Dto.SqsDto.ReservationSqsDto;
import com.chefconnect.reservationservice.services.Dto.SqsDto.RestaurantSqsDto;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

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

    public List<AvailableTablesResponseDto> getAvailableTables(UUID restaurantId, String date) {
        LocalDate requestedDate = LocalDate.parse(date);

        int totalTables = restaurantService.getTotalNumberOfTables(restaurantId);

        List<AvailableTablesResponseDto> availability = new ArrayList<>();

        String openingHourString = restaurantService.getRestaurant(restaurantId).getOpenTime();
        String [] openingHourStringParts = openingHourString.split(":");
        String closingHourString = restaurantService.getRestaurant(restaurantId).getCloseTime();
        String [] closingHourStringParts = closingHourString.split(":");
        int openingHour = Integer.parseInt(openingHourStringParts[0]);
        int closingHour = Integer.parseInt(closingHourStringParts[0]);
        
        for (int hour = openingHour; hour < closingHour; hour++) {
            LocalDateTime startOfHour = requestedDate.atTime(hour, 0);
            LocalDateTime endOfHour = startOfHour.plusHours(1);
        
            long reservedTablesCount = tableReservationRepository.countReservedTables(
                    restaurantId, startOfHour, endOfHour);
        
            int availableTablesCount = totalTables - (int) reservedTablesCount;
        
            AvailableTablesResponseDto responseDto = new AvailableTablesResponseDto();
            responseDto.setTimeSpan(startOfHour.toLocalTime().toString());
            responseDto.setValue(availableTablesCount);
        
            availability.add(responseDto);
        }
        
        return availability;
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

    private ReservationDto convertToDto(Reservation reservation) {
        return new ReservationDto(
            reservation.getId(),
            restaurantService.getRestaurant(reservation.getRestaurantId()).getAddress(),
            reservation.getTableReservations().size(),
            reservation.getDate(),
            reservation.getReservationStatus()
        );
    }

    private UUID getUserId(){
        // Pobieramy użytkownika z Cognito
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        Jwt jwt = (Jwt) authentication.getPrincipal();
        String userIdString = jwt.getClaimAsString("sub");
        return UUID.fromString(userIdString);
    }   
}
