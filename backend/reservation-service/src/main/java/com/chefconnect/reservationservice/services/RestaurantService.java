package com.chefconnect.reservationservice.services;

import java.util.Collection;
import java.util.List;
import java.util.UUID;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.ParameterizedTypeReference;
import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.oauth2.jwt.Jwt;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestClientException;
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

        HttpHeaders headers = new HttpHeaders();
        headers.set("Authorization", "Bearer " + getToken());

        HttpEntity<Void> requestEntity = new HttpEntity<>(headers);

        try {
            ResponseEntity<List<TableDto>> response = restTemplate.exchange(
                url,
                HttpMethod.GET,
                requestEntity,
                new ParameterizedTypeReference<List<TableDto>>() {}
            );

            if (response.getStatusCode() == HttpStatus.OK && response.getBody() != null)
                return response.getBody().size();
            else
                throw new IllegalStateException("Nieoczekiwany status odpowiedzi lub puste ciało odpowiedzi: " + response.getStatusCode());

        }
        catch (RestClientException e) {
            throw new RuntimeException("Nie udało się pobrać danych restauracji z " + url, e);
        }
    }

    public List<TableDto> getAllTablesForRestaurant(UUID restaurantId) {
        String url = restaurantServiceUrl + "/prod/restaurants-service/tables/" + restaurantId;

        HttpHeaders headers = new HttpHeaders();
        headers.set("Authorization", "Bearer " + getToken());

        HttpEntity<Void> requestEntity = new HttpEntity<>(headers);

        try {
            ResponseEntity<List<TableDto>> response = restTemplate.exchange(
                url,
                HttpMethod.GET,
                requestEntity,
                new ParameterizedTypeReference<List<TableDto>>() {}
            );

            if (response.getStatusCode() == HttpStatus.OK && response.getBody() != null)
                return response.getBody();
            else
                throw new IllegalStateException("Nieoczekiwany status odpowiedzi lub puste ciało odpowiedzi: " + response.getStatusCode());

        }
        catch (RestClientException e) {
            throw new RuntimeException("Nie udało się pobrać danych restauracji z " + url, e);
        }
    }


    public RestaurantDto getRestaurant(UUID restaurantId) {
        String url = restaurantServiceUrl + "/prod/restaurants-service/restaurants/" + restaurantId;
    
        HttpHeaders headers = new HttpHeaders();
        headers.set("Authorization", "Bearer " + getToken());
    
        HttpEntity<Void> requestEntity = new HttpEntity<>(headers);
    
        try {
            ResponseEntity<RestaurantDto> response = restTemplate.exchange(
                url,
                HttpMethod.GET,
                requestEntity,
                RestaurantDto.class
            );
    
            if (response.getStatusCode() == HttpStatus.OK && response.getBody() != null) {
                return response.getBody();
            } else {
                throw new IllegalStateException(
                    "Nieoczekiwany status odpowiedzi lub puste ciało odpowiedzi: " + response.getStatusCode()
                );
            }
        }
        catch (RestClientException e) {
            throw new RuntimeException("Nie udało się pobrać danych restauracji z " + url, e);
        }
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