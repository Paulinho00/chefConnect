package com.chefconnect.reservationservice.controller;

import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import com.chefconnect.reservationservice.exceptions.InvalidReservationStatusException;
import com.chefconnect.reservationservice.exceptions.OpinionAlreadyExistsException;
import com.chefconnect.reservationservice.services.CustomerOpinionService;
import com.chefconnect.reservationservice.services.Dto.AvarageRestaurantRateResponseDto;
import com.chefconnect.reservationservice.services.Dto.CustomerOpinionRequestDto;
import com.chefconnect.reservationservice.services.Dto.MessageResponseDto;

@RestController
@CrossOrigin(origins = "*")
@RequestMapping("/opinions")
public class CustomerOpinionController {

    private final CustomerOpinionService customerOpinionService;

    public CustomerOpinionController(CustomerOpinionService customerOpinionService) {
        this.customerOpinionService = customerOpinionService;
    }

    @PostMapping
    public ResponseEntity<?> addOpinion(@RequestBody CustomerOpinionRequestDto request) {
        try {
            MessageResponseDto message = customerOpinionService.addOpinion(request);
            return new ResponseEntity<>(message, HttpStatus.CREATED);
        } catch (InvalidReservationStatusException ex) {
            return buildErrorResponse(HttpStatus.BAD_REQUEST, ex.getMessage());
        } catch (OpinionAlreadyExistsException ex) {
            return buildErrorResponse(HttpStatus.CONFLICT, ex.getMessage());
        } catch (IllegalArgumentException ex) {
            return buildErrorResponse(HttpStatus.NOT_FOUND, ex.getMessage());
        }
    }

    @GetMapping("/average-rating")
    public ResponseEntity<?> getAverageRating(@RequestParam UUID restaurantId) {
        try {
            double averageRating = customerOpinionService.calculateAverageRatingForRestaurant(restaurantId);
            
            int roundedAverage = (int)((averageRating % 1 >= 0.5) ? Math.ceil(averageRating) : Math.floor(averageRating));
            
            return new ResponseEntity<>(new AvarageRestaurantRateResponseDto(roundedAverage), HttpStatus.OK);
        } catch (Exception e) {
            return buildErrorResponse(HttpStatus.INTERNAL_SERVER_ERROR, "Wystąpił błąd przy obliczaniu średniej oceny.");
        }
    }

    private ResponseEntity<Map<String, String>> buildErrorResponse(HttpStatus status, String errorMessage) {
        Map<String, String> errorResponse = new HashMap<>();
        errorResponse.put("errorMessage", errorMessage);
        return ResponseEntity.status(status).body(errorResponse);
    }
}
