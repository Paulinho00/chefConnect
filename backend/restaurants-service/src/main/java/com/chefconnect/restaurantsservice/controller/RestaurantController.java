package com.chefconnect.restaurantsservice.controller;

import com.chefconnect.restaurantsservice.service.RestaurantManagementService;
import com.chefconnect.restaurantsservice.service.RestaurantQueryService;
import com.chefconnect.restaurantsservice.service.dto.RestaurantDto;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.Collection;

@RestController
@CrossOrigin(origins = "*")
@RequestMapping("/restaurants")
@AllArgsConstructor(onConstructor_ = @Autowired, access = AccessLevel.PROTECTED)
public class RestaurantController {

    private final RestaurantQueryService restaurantQueryService;
    private final RestaurantManagementService restaurantManagementService;

    @GetMapping("/all")
    @PreAuthorize("isAuthenticated()")
    public ResponseEntity<Collection<RestaurantDto>> getAllRestaurants() {
        return ResponseEntity.ok(restaurantQueryService.getAllRestaurants());
    }
}
