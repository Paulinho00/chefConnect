package com.chefconnect.restaurantsservice.controller;

import java.util.Collection;
import java.util.UUID;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.chefconnect.restaurantsservice.domain.RestaurantTable;
import com.chefconnect.restaurantsservice.service.RestaurantTableService;
import com.chefconnect.restaurantsservice.service.dto.RestaurantTableDto;

import lombok.AllArgsConstructor;
import lombok.AccessLevel;

@RestController
@CrossOrigin(origins = "*")
@RequestMapping("tables")
@AllArgsConstructor(onConstructor_ = @Autowired, access = AccessLevel.PROTECTED)
public class RestaurantTableController {

    private final RestaurantTableService tableService;

    @GetMapping("/{restaurantId}")
    @PreAuthorize("isAuthenticated()")
    public ResponseEntity<Collection<RestaurantTableDto>> getAllTablesForRestaurant(@PathVariable UUID restaurantId) {
        return ResponseEntity.ok(tableService.getAllTablesForRestaurant(restaurantId));
    }

    @GetMapping
    @PreAuthorize("isAuthenticated()")
    public ResponseEntity<Collection<RestaurantTable>> getAllTables() {
        return ResponseEntity.ok(tableService.getAllTables());
    }

}
