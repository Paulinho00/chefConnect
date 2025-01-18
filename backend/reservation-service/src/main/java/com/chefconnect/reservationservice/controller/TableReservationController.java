package com.chefconnect.reservationservice.controller;

import java.time.LocalDateTime;
import java.util.List;
import java.util.UUID;

import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.chefconnect.reservationservice.domain.TableReservation;
import com.chefconnect.reservationservice.services.TableReservationService;
import com.chefconnect.reservationservice.services.Dto.AvailableTablesResponseDto;
import com.chefconnect.reservationservice.services.Dto.RestaurantServicesDto.TableDto;

@RestController
@CrossOrigin(origins = "*")
@RequestMapping("/tables")
public class TableReservationController {

    private final TableReservationService tableReservationService;

    public TableReservationController(TableReservationService tableReservationService) {
        this.tableReservationService = tableReservationService;
    }

    @GetMapping("/available-tables/{restaurantId}/{date}")
    public List<AvailableTablesResponseDto> getAvailableTables(
            @PathVariable UUID restaurantId,
            @PathVariable String date) {
        
        String trimmedDate = date.trim();
        return tableReservationService.getAvailableTables(restaurantId, trimmedDate);
    }

    @GetMapping("/available-tables-specific-hour/{restaurantId}/{date}")
    public List<TableDto> getAvailableTablesForSpecificHour(
            @PathVariable UUID restaurantId,
            @PathVariable @DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME) LocalDateTime date) {
        
        return tableReservationService.getAvailableTablesForSpecificHour(restaurantId, date);
    }
}