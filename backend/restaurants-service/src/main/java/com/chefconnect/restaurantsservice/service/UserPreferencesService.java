package com.chefconnect.restaurantsservice.service;

import com.chefconnect.restaurantsservice.domain.Restaurant;
import com.chefconnect.restaurantsservice.domain.UserPreferences;
import com.chefconnect.restaurantsservice.repository.RestaurantRepository;
import com.chefconnect.restaurantsservice.repository.UserPreferencesRepository;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.nio.charset.StandardCharsets;
import java.util.Collection;
import java.util.List;
import java.util.UUID;

@Service
@RequiredArgsConstructor(onConstructor_ = @Autowired, access = AccessLevel.PROTECTED)
public class UserPreferencesService {

    private final RestaurantRepository restaurantRepository;
    private final UserPreferencesRepository userPreferencesRepository;

    public void addRestaurantToUserPreferences(UUID restaurantId, String userId) {
        var restaurant = restaurantRepository.findById(restaurantId)
                .orElseThrow(() -> new IllegalArgumentException("Restaurant not found"));

        var userUUID = UUID.fromString(userId);

        var userPreferencesOpt = userPreferencesRepository.findById(userUUID);
        if (userPreferencesOpt.isPresent()) {
            var userPreferences = userPreferencesOpt.get();
            userPreferences.getFavoriteRestaurants().add(restaurant);
            userPreferencesRepository.save(userPreferences);
        }

        userPreferencesRepository.save(
                new UserPreferences(userUUID, restaurant)
        );
    }

    public void removeRestaurantFromUserPreferences(UUID restaurantId, String userId) {
        var userUUID = UUID.fromString(userId);
        var userPreferencesOpt = userPreferencesRepository.findById(userUUID);
        if (userPreferencesOpt.isPresent()) {
            var userPreferences = userPreferencesOpt.get();
            userPreferences.getFavoriteRestaurants().removeIf(r -> r.getId().equals(restaurantId));
            userPreferencesRepository.save(userPreferences);
        }
    }

    public Collection<UUID> getFavoriteRestaurants(String userId) {
        return userPreferencesRepository.findById(UUID.fromString(userId))
                .map(UserPreferences::getFavoriteRestaurants)
                .map(rs -> rs.stream()
                        .map(Restaurant::getId)
                        .toList()
                ).orElseGet(List::of);
    }
}
