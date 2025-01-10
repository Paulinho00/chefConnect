package com.chefconnect.restaurantsservice.repository;

import com.chefconnect.restaurantsservice.domain.RestaurantTable;

import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Collection;
import java.util.UUID;

public interface TableRepository extends JpaRepository<RestaurantTable, UUID> {
    Collection<RestaurantTable> findByRestaurantId(UUID restaurantId);
}
