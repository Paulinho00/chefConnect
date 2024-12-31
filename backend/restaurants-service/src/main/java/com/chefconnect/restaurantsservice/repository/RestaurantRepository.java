package com.chefconnect.restaurantsservice.repository;

import com.chefconnect.restaurantsservice.domain.Restaurant;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface RestaurantRepository extends JpaRepository<Restaurant, UUID> {
}
