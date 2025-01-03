package com.chefconnect.reservationservice.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.chefconnect.reservationservice.models.Reservation;

public interface ReservationRepository extends JpaRepository<Reservation, Long> {
    
}
