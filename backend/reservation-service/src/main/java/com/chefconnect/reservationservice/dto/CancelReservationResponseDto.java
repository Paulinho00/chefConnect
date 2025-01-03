package com.chefconnect.reservationservice.Dto;

public class CancelReservationResponseDto {
    private String message;

    public CancelReservationResponseDto(String message) {
        this.message = message;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}
