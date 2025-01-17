package com.chefconnect.restaurantsservice.service;

import java.util.Collection;
import java.util.UUID;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.chefconnect.restaurantsservice.domain.RestaurantTable;
import com.chefconnect.restaurantsservice.repository.RestaurantRepository;
import com.chefconnect.restaurantsservice.repository.RestaurantTableRepository;
import com.chefconnect.restaurantsservice.service.dto.RestaurantDto;
import com.chefconnect.restaurantsservice.service.dto.RestaurantTableDto;

import jakarta.persistence.EntityNotFoundException;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import lombok.AccessLevel;

@Slf4j
@Service
@RequiredArgsConstructor(onConstructor_ = @Autowired, access = AccessLevel.PROTECTED)
public class RestaurantTableService {

    private final RestaurantTableRepository tableRepository;
    private final RestaurantRepository restaurantRepository;
    
    public Collection<RestaurantTableDto> getAllTablesForRestaurant(UUID restaurantId) {
        if (!restaurantRepository.existsById(restaurantId)) {
            throw new EntityNotFoundException("Nie znaleziono restauracji o id: " + restaurantId);
        }
        Collection<RestaurantTableDto> tables = tableRepository.findByRestaurantId(restaurantId).
            stream().
            map(RestaurantTableDto::fromEntity).
            toList();
            
        if (tables.isEmpty()) {
            log.warn("Nie znaleziono stolików dla restauracji o id: {}", restaurantId);
        }
        return tables;
    }

    public Collection<RestaurantTable> getAllTables() {
        Collection<RestaurantTable> tables = tableRepository.findAll();
        if (tables.isEmpty()) {
            log.warn("Nie znaleziono stolików");
        }
        return tables;
    }
}
