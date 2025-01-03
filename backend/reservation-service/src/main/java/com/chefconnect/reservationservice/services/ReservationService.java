package com.chefconnect.reservationservice.services;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.oauth2.jwt.Jwt;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Service;

import com.chefconnect.reservationservice.Dto.AvailableTablesResponseDto;
import com.chefconnect.reservationservice.Dto.CancelReservationResponseDto;
import com.chefconnect.reservationservice.Dto.ReservationDto;
import com.chefconnect.reservationservice.exceptions.ReservationNotFoundException;
import com.chefconnect.reservationservice.models.Reservation;
import com.chefconnect.reservationservice.models.ReservationStatus;
import com.chefconnect.reservationservice.repository.ReservationRepository;
import com.chefconnect.reservationservice.repository.TableReservationRepository;

@Service
public class ReservationService {
    private ReservationRepository reservationRepository;
    private TableReservationRepository tableReservationRepository;
    private RestaurantService restaurantService;

    public ReservationService(
    ReservationRepository reservationRepository, 
    TableReservationRepository tableReservationRepository,
    RestaurantService restaurantService){
        this.reservationRepository = reservationRepository;
        this.tableReservationRepository = tableReservationRepository;
        this.restaurantService = restaurantService;
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

            return reservationRepository.save(reservation);
        } catch (Exception e) {
            throw new IllegalArgumentException("Błąd przy tworzeniu rezerwacji: " + e.getMessage());
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

    public CancelReservationResponseDto cancelReservation(UUID reservationId) {
        Reservation reservation = reservationRepository.findById(reservationId)
                .orElseThrow(() -> new ReservationNotFoundException(reservationId));
        
        reservation.setReservationStatus(ReservationStatus.CANCELLED);
        
        reservationRepository.save(reservation);
        
        return new CancelReservationResponseDto("Pomyślnie anulowano");
    }

    private String formatTimeSpan(LocalDateTime dateTime) {
        return dateTime.toLocalTime().format(DateTimeFormatter.ofPattern("HH:mm:ss"));
    }

    private ReservationDto convertToDto(Reservation reservation) {
        return new ReservationDto(
            reservation.getId(),
            restaurantService.getRestaurantAddress(reservation.getRestaurantId()),
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
