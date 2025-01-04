package com.chefconnect.reservationservice.exceptions;

public class OpinionAlreadyExistsException extends RuntimeException {
    
    public OpinionAlreadyExistsException(String message) {
        super(message);
    }
}
