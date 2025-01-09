package com.chefconnect.reservationservice.services;

import java.util.UUID;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.oauth2.jwt.Jwt;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;
import com.chefconnect.reservationservice.services.Dto.RestaurantServicesDto.RestaurantDto;

@Service
public class RestaurantService {

    @Value("${restaurants.service.url}")
    private String restaurantServiceUrl;

    private final RestTemplate restTemplate;

    public RestaurantService(RestTemplate restTemplate) {
        this.restTemplate = restTemplate;
    }

    // Mockup (komunikacja z mikroserwisem dla restauracji)
    public int getTotalNumberOfTables(UUID restaurantId) {
        return 50;
    }

    // public String getRestaurantAddress(@PathVariable UUID restaurantId) {

    //     // Mockup (komunikacja z mikroserwisem dla restauracji)
    //     AddressDto address = new AddressDto(
    //         "Main St",
    //         123,
    //         "45B",
    //         "01-234",
    //         "Warsaw"
    //     );

    //     String addressString = address.getStreet() + " " +
    //                         address.getStreetNumber() + ", " +
    //                         address.getFlatNumber() + ", " +
    //                         address.getPostalCode() + ", " +
    //                         address.getCity();

    //     return addressString;
    // }

    public String getRestaurantAddress(UUID restaurantId) {
        String url = restaurantServiceUrl + "/prod/restaurants-service/restaurants/" + restaurantId;

        // Tworzenie nagłówków i dodanie tokena
        HttpHeaders headers = new HttpHeaders();
        headers.set("Authorization", "Bearer " + getToken());

        HttpEntity<Void> requestEntity = new HttpEntity<>(headers);

        ResponseEntity<RestaurantDto> response = restTemplate.exchange(
            url,
            HttpMethod.GET,
            requestEntity,
            RestaurantDto.class
        );
        return response.getBody().getAddress();
    }

    private String getToken() {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        
        if (authentication == null || !(authentication.getPrincipal() instanceof Jwt)) {
            throw new IllegalStateException("Brak tokenu w kontekście bezpieczeństwa");
        }
    
        Jwt jwt = (Jwt) authentication.getPrincipal();
        return jwt.getTokenValue(); // Pobranie całego tokena jako string
    }
}