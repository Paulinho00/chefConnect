package com.chefconnect.reservationservice.services;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

import org.springframework.core.ParameterizedTypeReference;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.RestController;

import com.chefconnect.reservationservice.domain.Reservation;
import com.chefconnect.reservationservice.domain.ReservationStatus;
import com.chefconnect.reservationservice.domain.TableReservation;
import com.chefconnect.reservationservice.repository.ReservationRepository;
import com.chefconnect.reservationservice.repository.TableReservationRepository;
import com.chefconnect.reservationservice.services.Dto.AvailableTablesResponseDto;
import com.chefconnect.reservationservice.services.Dto.RestaurantServicesDto.TableDto;

@Service
public class TableReservationService {

    private final TableReservationRepository tableReservationRepository;
    private final ReservationRepository reservationRepository;
    private final RestaurantService restaurantService;

    public TableReservationService(TableReservationRepository tableReservationRepository, RestaurantService restaurantService, ReservationRepository reservationRepository) {
        this.tableReservationRepository = tableReservationRepository;
        this.restaurantService = restaurantService;
        this.reservationRepository = reservationRepository;
    }

    public List<AvailableTablesResponseDto> getAvailableTables(UUID restaurantId, String date) {
        LocalDate requestedDate = LocalDate.parse(date);

        int totalTables = restaurantService.getTotalNumberOfTables(restaurantId);

        List<AvailableTablesResponseDto> availability = new ArrayList<>();

        String openingHourString = restaurantService.getRestaurant(restaurantId).getOpenTime();
        String [] openingHourStringParts = openingHourString.split(":");
        String closingHourString = restaurantService.getRestaurant(restaurantId).getCloseTime();
        String [] closingHourStringParts = closingHourString.split(":");
        int openingHour = Integer.parseInt(openingHourStringParts[0]);
        int closingHour = Integer.parseInt(closingHourStringParts[0]);
        
        for (int hour = openingHour; hour < closingHour; hour++) {
            LocalDateTime startOfHour = requestedDate.atTime(hour, 0);
        
            long reservedTablesCount = tableReservationRepository.countReservedTables(
                    restaurantId, startOfHour);
        
            int availableTablesCount = totalTables - (int) reservedTablesCount;
        
            AvailableTablesResponseDto responseDto = new AvailableTablesResponseDto();
            responseDto.setTimeSpan(startOfHour.toLocalTime().toString());
            responseDto.setValue(availableTablesCount);
        
            availability.add(responseDto);
        }
        
        return availability;
    }

    public List<TableDto> getAvailableTablesForSpecificHour(UUID restaurantId, LocalDateTime dateTime) {
        List<TableDto> allTables = restaurantService.getAllTablesForRestaurant(restaurantId);

        return filterAvailableTables(allTables, restaurantId, dateTime);
    }

    private List<TableDto> filterAvailableTables(List<TableDto> allTables, UUID restaurantId, LocalDateTime dateTime) {
        List<Reservation> reservations = reservationRepository.findAllByDateAndRestaurantId(dateTime, restaurantId);

        return allTables.stream()
                .filter(table -> isTableAvailable(table, reservations))
                .collect(Collectors.toList());
    }

    private boolean isTableAvailable(TableDto table, List<Reservation> reservations) {
        return reservations.stream()
                .noneMatch(reservation -> reservation.getTableReservations().stream()
                        .anyMatch(reservedTable -> reservedTable.getTableId().equals(table.getId()))
                        && reservation.getApprovingWorkerId() != null
                        && reservation.getReservationStatus() == ReservationStatus.CONFIRMED);
    }
}