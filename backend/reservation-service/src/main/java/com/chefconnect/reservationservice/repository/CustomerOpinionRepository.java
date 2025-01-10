package com.chefconnect.reservationservice.repository;

import java.util.List;
import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;

import com.chefconnect.reservationservice.domain.CustomerOpinion;
import com.chefconnect.reservationservice.domain.Reservation;

public interface CustomerOpinionRepository extends JpaRepository<CustomerOpinion, UUID> {
}
