package com.chefconnect.restaurantsservice.service.dto;

import com.chefconnect.restaurantsservice.domain.Restaurant;
import com.chefconnect.restaurantsservice.domain.RestaurantTable;

public record RestaurantDto(
        String id,
        int numberOfSeats,
        String address,
        String name,
        String openTime,
        String closeTime
) {

    public static RestaurantDto fromEntity(Restaurant restaurant) {
        return new RestaurantDto(
                restaurant.getId().toString(),
                restaurant.getTables().stream().mapToInt(RestaurantTable::getNumberOfSeats).sum(),
                restaurant.getAddress().toString(),
                restaurant.getName(),
                restaurant.getOpenTime(),
                restaurant.getCloseTime()
        );
    }
}
