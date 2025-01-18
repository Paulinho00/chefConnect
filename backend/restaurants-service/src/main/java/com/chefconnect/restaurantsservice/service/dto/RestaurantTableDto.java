package com.chefconnect.restaurantsservice.service.dto;

import com.chefconnect.restaurantsservice.domain.RestaurantTable;

import java.util.UUID;

public record RestaurantTableDto(
        UUID id,
        int numberOfSeats,
        UUID restaurantId
) {

    public static RestaurantTableDto fromEntity(RestaurantTable restaurantTable) {
        return new RestaurantTableDto(
                restaurantTable.getId(),
                restaurantTable.getNumberOfSeats(),
                restaurantTable.getRestaurant().getId()
        );
    }
}
