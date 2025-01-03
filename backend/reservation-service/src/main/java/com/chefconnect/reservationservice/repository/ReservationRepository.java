package com.chefconnect.reservationservice.repository;

import java.util.List;
import java.util.Optional;
import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;

import com.chefconnect.reservationservice.models.Reservation;

public interface ReservationRepository extends JpaRepository<Reservation, Long> {
    List<Reservation> findByUserIdAndIsDeletedFalse(UUID userId);
    Optional<Reservation> findById(UUID reservationId);
}
