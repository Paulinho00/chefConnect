package com.chefconnect.reservationservice.repository;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Optional;
import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;

import com.chefconnect.reservationservice.domain.Reservation;
import com.chefconnect.reservationservice.domain.ReservationStatus;

public interface ReservationRepository extends JpaRepository<Reservation, Long> {
    List<Reservation> findByUserIdAndIsDeletedFalse(UUID userId);
    Optional<Reservation> findById(UUID reservationId);
    List<Reservation> findByRestaurantId(UUID restaurantId);
    List<Reservation> findAllByDateAndRestaurantId(LocalDateTime date, UUID restaurantId);
}
