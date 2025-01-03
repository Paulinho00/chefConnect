package com.chefconnect.reservationservice.controller;

import java.util.HashMap;
import java.util.Map;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.chefconnect.reservationservice.dto.ReservationRequestDto;
import com.chefconnect.reservationservice.models.Reservation;
import com.chefconnect.reservationservice.services.ReservationService;

@RestController
@RequestMapping("/reservations")
public class ReservationController {
    private final ReservationService reservationService;

    public ReservationController(ReservationService reservationService) {
        this.reservationService = reservationService;
    }

    @PostMapping
    public ResponseEntity<?> createReservation(@RequestBody ReservationRequestDto request) {
        try {
            Reservation reservation = reservationService.createReservation(
                request.getRestaurantId(), 
                request.getDate(), 
                request.getNumberOfPeople(),
                request.getUserId()
            );

            Map<String, String> response = new HashMap<>();
            response.put("reservationId", reservation.getId().toString());

            return ResponseEntity.status(HttpStatus.CREATED).body(response);
        } catch (Exception e) {
            return buildErrorResponse(HttpStatus.BAD_REQUEST, e.getMessage());
        }
    }

    private ResponseEntity<Map<String, String>> buildErrorResponse(HttpStatus status, String errorMessage) {
        Map<String, String> errorResponse = new HashMap<>();
        errorResponse.put("errorMessage", errorMessage);
        return ResponseEntity.status(status).body(errorResponse);
    }
}
