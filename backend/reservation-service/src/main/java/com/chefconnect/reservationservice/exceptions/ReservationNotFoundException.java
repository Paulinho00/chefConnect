package com.chefconnect.reservationservice.exceptions;

import java.util.UUID;

public class ReservationNotFoundException extends RuntimeException {
    public ReservationNotFoundException(UUID reservationId) {
        super("Rezerwacja o ID " + reservationId + " nie została pomyślnie anulowana");
    }
} 