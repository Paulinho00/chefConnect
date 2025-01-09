package com.chefconnect.analyticsservice.service.dto;

import java.time.Instant;

public record ReservationEventDto(
        Instant date,
        RestaurantDto restaurant,
        int numberOfPeople
) {
}
