package com.chefconnect.analyticsservice.repository;

import com.chefconnect.analyticsservice.domain.ReservationEvent;
import com.chefconnect.analyticsservice.service.dto.EventCountDto;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.time.Instant;
import java.util.List;
import java.util.UUID;

public interface ReservationEventRepository extends JpaRepository<ReservationEvent, UUID> {

    @Query("SELECT new com.chefconnect.analyticsservice.service.dto.EventCountDto(e.date, COUNT(e)) " +
            "FROM ReservationEvent e " +
            "WHERE e.restaurant.id = :restaurantId " +
            "AND e.date BETWEEN :startDate AND :endDate " +
            "GROUP BY e.date " +
            "ORDER BY e.date ASC")
    List<EventCountDto> countEventsByDateRangeAndRestaurant(
            @Param("restaurantId") UUID restaurantId,
            @Param("startDate") Instant startDate,
            @Param("endDate") Instant endDate
    );
}
