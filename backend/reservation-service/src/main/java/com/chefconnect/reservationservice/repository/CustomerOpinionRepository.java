package com.chefconnect.reservationservice.repository;

import java.util.List;
import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;
import com.chefconnect.reservationservice.models.CustomerOpinion;
import com.chefconnect.reservationservice.models.Reservation;

public interface CustomerOpinionRepository extends JpaRepository<CustomerOpinion, UUID> {
}
