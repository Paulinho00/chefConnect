package com.chefconnect.analyticsservice.repository;

import com.chefconnect.analyticsservice.domain.ReservationEvent;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface ReservationEventRepository extends JpaRepository<ReservationEvent, UUID> {
}
