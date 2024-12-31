package com.chefconnect.restaurantsservice.service.dto;

import com.chefconnect.restaurantsservice.domain.Restaurant;
import com.chefconnect.restaurantsservice.domain.RestaurantTable;

import java.util.UUID;

public record RestaurantDto(
        UUID id,
        int numberOfSeats,
        String address,
        String name,
        String openTime,
        String closeTime
) {

    public static RestaurantDto fromEntity(Restaurant restaurant) {
        return new RestaurantDto(
                restaurant.getId(),
                restaurant.getTables().stream().mapToInt(RestaurantTable::getNumberOfSeats).sum(),
                restaurant.getAddress().toString(),
                restaurant.getName(),
                restaurant.getOpenTime(),
                restaurant.getCloseTime()
        );
    }
}
