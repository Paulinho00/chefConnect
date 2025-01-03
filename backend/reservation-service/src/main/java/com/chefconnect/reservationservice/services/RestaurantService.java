package com.chefconnect.reservationservice.services;

import java.util.UUID;

import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.PathVariable;

import com.chefconnect.reservationservice.Dto.AddressDto;

@Service
public class RestaurantService {

    // Mockup (komunikacja z mikroserwisem dla restauracji)
    public int getTotalNumberOfTables(UUID restaurantId) {
        return 50;
    }

    public String getRestaurantAddress(@PathVariable UUID restaurantId) {

        // Mockup (komunikacja z mikroserwisem dla restauracji)
        AddressDto address = new AddressDto(
            "Main St",
            123,
            "45B",
            "01-234",
            "Warsaw"
        );

        String addressString = address.getStreet() + " " +
                            address.getStreetNumber() + ", " +
                            address.getFlatNumber() + ", " +
                            address.getPostalCode() + ", " +
                            address.getCity();

        return addressString;
    }
}