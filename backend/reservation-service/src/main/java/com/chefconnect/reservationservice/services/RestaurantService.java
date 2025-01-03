package com.chefconnect.reservationservice.services;

import java.util.UUID;

import org.springframework.stereotype.Service;

@Service
public class RestaurantService {

    // Mockup (komunikacja z mikroserwisem dla restauracji)
    public int getTotalNumberOfTables(UUID restaurantId) {
        return 50;
    }
}