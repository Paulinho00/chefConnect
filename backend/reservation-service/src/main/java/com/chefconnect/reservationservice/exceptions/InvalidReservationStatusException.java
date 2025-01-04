package com.chefconnect.reservationservice.exceptions;

public class InvalidReservationStatusException extends RuntimeException {
    public InvalidReservationStatusException(String message) {
        super(message);
    }
}
