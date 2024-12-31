package com.chefconnect.restaurantsservice.service;

import com.chefconnect.restaurantsservice.repository.RestaurantRepository;
import com.chefconnect.restaurantsservice.repository.UserPreferencesRepository;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor(onConstructor_ = @Autowired, access = AccessLevel.PROTECTED)
public class RestaurantManagementService {

    private final RestaurantRepository restaurantRepository;
    private final UserPreferencesRepository userPreferencesRepository;
}
