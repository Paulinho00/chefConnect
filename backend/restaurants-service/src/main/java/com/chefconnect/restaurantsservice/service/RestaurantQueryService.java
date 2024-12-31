package com.chefconnect.restaurantsservice.service;

import com.chefconnect.restaurantsservice.repository.RestaurantRepository;
import com.chefconnect.restaurantsservice.service.dto.RestaurantDto;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Collection;

@Service
@RequiredArgsConstructor(onConstructor_ = @Autowired, access = AccessLevel.PROTECTED)
public class RestaurantQueryService {

    private final RestaurantRepository restaurantRepository;

    public Collection<RestaurantDto> getAllRestaurants() {
        return restaurantRepository.findAll()
                .stream()
                .map(RestaurantDto::fromEntity)
                .toList();
    }
}
