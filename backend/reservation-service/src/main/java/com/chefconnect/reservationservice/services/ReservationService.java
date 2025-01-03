package com.chefconnect.reservationservice.services;

import java.time.LocalDateTime;
import java.util.UUID;

import com.chefconnect.reservationservice.models.Reservation;
import com.chefconnect.reservationservice.repository.ReservationRepository;

public class ReservationService {
    private ReservationRepository reservationRepository;

    public ReservationService(ReservationRepository reservationRepository){
        this.reservationRepository = reservationRepository;
    }

    public Reservation createReservation(UUID restaurantId, String date, int numberOfPeople, UUID userId) {
        try {
            LocalDateTime reservationDate = LocalDateTime.parse(date);

            // User user = userRepository.findById(userId)
            //     .orElseThrow(() -> new IllegalArgumentException("Użytkownik o podanym ID nie istnieje."));

            Reservation reservation = new Reservation();
            reservation.setRestaurantId(restaurantId);
            reservation.setDate(reservationDate);
            reservation.setNumberOfPeople(numberOfPeople);
            reservation.setApproved(false);
            reservation.setDeleted(false);
            reservation.setUserId(userId);

            return reservationRepository.save(reservation);
        } catch (Exception e) {
            throw new IllegalArgumentException("Błąd przy tworzeniu rezerwacji: " + e.getMessage());
        }
    }
}
