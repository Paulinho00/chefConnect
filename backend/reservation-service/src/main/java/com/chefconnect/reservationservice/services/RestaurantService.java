package com.chefconnect.reservationservice.services;

import java.util.Collection;
import java.util.UUID;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.ParameterizedTypeReference;
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
import com.chefconnect.reservationservice.services.Dto.RestaurantServicesDto.TableDto;

@Service
public class RestaurantService {

    @Value("${restaurants.service.url}")
    private String restaurantServiceUrl;

    private final RestTemplate restTemplate;

    public RestaurantService(RestTemplate restTemplate) {
        this.restTemplate = restTemplate;
    }

    public int getTotalNumberOfTables(UUID restaurantId) {

        String url = restaurantServiceUrl + "/prod/restaurants-service/tables/" + restaurantId;

        ResponseEntity<Collection<TableDto>> response = sendRequest(
            url,
            new ParameterizedTypeReference<Collection<TableDto>>() {}
        );
        
        return response.getBody().size();
    }

    public String getRestaurantAddress(UUID restaurantId) {
        String url = restaurantServiceUrl + "/prod/restaurants-service/restaurants/" + restaurantId;

        ResponseEntity<RestaurantDto> response = sendRequest(
            url,
            new ParameterizedTypeReference<RestaurantDto>() {}
        );

        return response.getBody().getAddress();
    }

    private <T> ResponseEntity<T> sendRequest(String url, ParameterizedTypeReference<T> responseType) {

        // Tworzenie nagłówków i dodanie tokena
        HttpHeaders headers = new HttpHeaders();
        headers.set("Authorization", "Bearer " + getToken());

        HttpEntity<Void> requestEntity = new HttpEntity<>(headers);

        ResponseEntity<T> response = restTemplate.exchange(
            url,
            HttpMethod.GET,
            requestEntity,
            responseType
        );

        return response;
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