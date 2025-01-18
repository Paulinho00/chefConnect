package com.chefconnect.reservationservice.controller;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.chefconnect.reservationservice.domain.Reservation;
import com.chefconnect.reservationservice.domain.TableReservation;
import com.chefconnect.reservationservice.exceptions.ReservationNotFoundException;
import com.chefconnect.reservationservice.services.ReservationService;
import com.chefconnect.reservationservice.services.Dto.MessageResponseDto;
import com.chefconnect.reservationservice.services.Dto.ReservationDto;
import com.chefconnect.reservationservice.services.Dto.ReservationRequestDto;

@RestController
@CrossOrigin(origins = "*")
@RequestMapping("/reservations")
public class ReservationController {
    private final ReservationService reservationService;

    public ReservationController(ReservationService reservationService) {
        this.reservationService = reservationService;
    }

    @PostMapping(value = "/reserve")
    public ResponseEntity<?> createReservation(@RequestBody ReservationRequestDto request) {
        try {
            Reservation reservation = reservationService.createReservation(
                request.getRestaurantId(), 
                request.getDate(), 
                request.getNumberOfPeople()
            );

            Map<String, String> response = new HashMap<>();
            response.put("reservationId", reservation.getId().toString());

            return ResponseEntity.status(HttpStatus.CREATED).body(response);
        } catch (Exception e) {
            return buildErrorResponse(HttpStatus.BAD_REQUEST, e.getMessage());
        }
    }

    @GetMapping
    public ResponseEntity<List<ReservationDto>> getUserReservations() {
        List<ReservationDto> reservations = reservationService.getUserReservations();

        return ResponseEntity.ok(reservations);
    }

    @PutMapping("/cancel/{reservationId}")
    public ResponseEntity<MessageResponseDto> cancelReservation(@PathVariable UUID reservationId) {
        try {
            MessageResponseDto response = reservationService.cancelReservation(reservationId);
            return ResponseEntity.ok(response);
        } catch (ReservationNotFoundException ex) {
            MessageResponseDto response = new MessageResponseDto(ex.getMessage());
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(response);
        }
    }

    @PutMapping("/confirm/{reservationId}")
    public ResponseEntity<MessageResponseDto> confirmReservation(
            @PathVariable UUID reservationId,
            @RequestBody List<UUID> tableIds) {

        reservationService.confirmReservation(reservationId, tableIds);

        MessageResponseDto response = new MessageResponseDto("Rezerwacja została pomyślnie potwierdzona.");
        return new ResponseEntity<>(response, HttpStatus.OK);
    }

    @GetMapping("/{restaurantId}/unconfirmed")
    public ResponseEntity<List<Reservation>> getAllUnconfirmedReservationsForRestaurant(@PathVariable UUID restaurantId) {
        List<Reservation> reservations = reservationService.getAllUnconfirmedReservationsForRestaurant(restaurantId);
        return ResponseEntity.ok(reservations);
    }

    private ResponseEntity<Map<String, String>> buildErrorResponse(HttpStatus status, String errorMessage) {
        Map<String, String> errorResponse = new HashMap<>();
        errorResponse.put("errorMessage", errorMessage);
        return ResponseEntity.status(status).body(errorResponse);
    }
}
