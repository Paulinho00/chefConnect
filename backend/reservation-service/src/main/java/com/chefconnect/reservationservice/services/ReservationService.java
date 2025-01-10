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
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Service;

import com.chefconnect.reservationservice.exceptions.ReservationNotFoundException;
import com.chefconnect.reservationservice.models.Reservation;
import com.chefconnect.reservationservice.models.ReservationStatus;
import com.chefconnect.reservationservice.repository.ReservationRepository;
import com.chefconnect.reservationservice.repository.TableReservationRepository;
import com.chefconnect.reservationservice.services.Dto.AvailableTablesResponseDto;
import com.chefconnect.reservationservice.services.Dto.MessageResponseDto;
import com.chefconnect.reservationservice.services.Dto.ReservationDto;
import com.chefconnect.reservationservice.services.Dto.RestaurantServicesDto.RestaurantDto;
import com.fasterxml.jackson.databind.ObjectMapper;

import io.awspring.cloud.sqs.operations.SqsTemplate;

@Service
public class ReservationService {

    @Value("${events.queues.event-queue}")
    private String queueUrl;

    private SqsTemplate sqsTemplate;
    private ReservationRepository reservationRepository;
    private TableReservationRepository tableReservationRepository;
    private RestaurantService restaurantService;

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
            this.sendToQueue(reservation, restaurantId, reservationDate);
            
            return reservationRepository.save(reservation);

        } catch (Exception e) {
            throw new IllegalArgumentException("Błąd przy tworzeniu rezerwacji: " + e.getMessage());
        }
    }

    private void sendToQueue(Reservation reservation, UUID restaurantId, LocalDateTime reservationDate){
        Map<String, Object> reservationEvent = new HashMap<>();
        RestaurantDto restaurant = restaurantService.getRestaurant(restaurantId);
    
        reservationEvent.put("date", reservationDate.atZone(ZoneId.systemDefault()).toInstant());
        reservationEvent.put("restaurant", new HashMap<String, Object>() {{
            put("id", restaurant.getId());
            put("name", restaurant.getName());
            put("address", restaurant.getAddress());
        }});
        reservationEvent.put("numberOfPeople", reservation.getNumberOfPeople());
    
        ObjectMapper objectMapper = new ObjectMapper();
        
        try {
            String messageBody = objectMapper.writeValueAsString(reservationEvent);
            sqsTemplate.send(queueUrl, messageBody);
        } catch (Exception e) {
            throw new RuntimeException("Błąd podczas serializacji do JSON: " + e.getMessage(), e);
        }
    }

    public List<AvailableTablesResponseDto> getAvailableTables(UUID restaurantId, String date) {
        LocalDate requestedDate = LocalDate.parse(date);

        int totalTables = restaurantService.getTotalNumberOfTables(restaurantId);

        List<AvailableTablesResponseDto> availability = new ArrayList<>();

        int openingHour = 10;
        int closingHour = 17;
        
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
