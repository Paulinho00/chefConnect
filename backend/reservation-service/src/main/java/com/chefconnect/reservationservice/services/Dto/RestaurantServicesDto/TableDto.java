package com.chefconnect.reservationservice.services.Dto.RestaurantServicesDto;

import java.util.UUID;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class TableDto {

    private UUID id;
    private int numberOfSeats;
    private UUID restaurantId;
}