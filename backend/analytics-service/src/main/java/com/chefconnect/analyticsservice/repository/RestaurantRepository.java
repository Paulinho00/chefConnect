package com.chefconnect.analyticsservice.repository;

import com.chefconnect.analyticsservice.domain.Restaurant;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface RestaurantRepository extends JpaRepository<Restaurant, UUID> {
}
