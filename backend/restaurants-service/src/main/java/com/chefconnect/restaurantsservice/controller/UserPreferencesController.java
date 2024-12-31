package com.chefconnect.restaurantsservice.controller;

import com.chefconnect.restaurantsservice.service.UserPreferencesService;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.oauth2.jwt.Jwt;
import org.springframework.web.bind.annotation.*;

import java.util.Collection;
import java.util.UUID;

@RestController
@CrossOrigin(origins = "*")
@RequestMapping("/user-preferences")
@AllArgsConstructor(onConstructor_ = @Autowired, access = AccessLevel.PROTECTED)
public class UserPreferencesController {

    private final UserPreferencesService userPreferencesService;

    @GetMapping("")
    @PreAuthorize("isAuthenticated()")
    public ResponseEntity<Collection<UUID>> getFavoriteRestaurants() {
        return ResponseEntity.ok(userPreferencesService.getFavoriteRestaurants(getUserIdFromToken()));
    }

    @PostMapping("/add/{restaurantId}")
    @PreAuthorize("isAuthenticated()")
    public ResponseEntity<Void> addRestaurantToUserPreferences(@PathVariable UUID restaurantId) {
        userPreferencesService.addRestaurantToUserPreferences(restaurantId, getUserIdFromToken());
        return ResponseEntity.ok().build();
    }

    @PostMapping("/remove/{restaurantId}")
    @PreAuthorize("isAuthenticated()")
    public ResponseEntity<Void> removeRestaurantFromUserPreferences(@PathVariable UUID restaurantId) {
        userPreferencesService.removeRestaurantFromUserPreferences(restaurantId, getUserIdFromToken());
        return ResponseEntity.ok().build();
    }

    private String getUserIdFromToken() {
        var authentication = SecurityContextHolder.getContext().getAuthentication();
        var jwt = (Jwt) authentication.getPrincipal();
        return jwt.getClaim("sub");
    }
}
