package com.chefconnect.analyticsservice.service.dto;

import java.util.UUID;

public record RestaurantDto(
        UUID id,
        String name,
        String address
) {
}
