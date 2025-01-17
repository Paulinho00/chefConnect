package com.chefconnect.analyticsservice.controller;

import com.chefconnect.analyticsservice.service.ReservationEventQueryService;
import com.chefconnect.analyticsservice.service.dto.EventCountDto;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.time.Instant;
import java.util.List;
import java.util.UUID;

@RestController
@CrossOrigin(origins = "*")
@RequestMapping("events")
@PreAuthorize("isAuthenticated()")
@AllArgsConstructor(onConstructor_ = @Autowired, access = AccessLevel.PROTECTED)
public class EventsQueryController {

    private final ReservationEventQueryService restaurantQueryService;

    @GetMapping("/{restaurantId}/reservation/counts")
    public ResponseEntity<List<EventCountDto>> getEventCounts(
            @PathVariable("restaurantId") UUID restaurantId,
            @RequestParam("startDate") @DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME) Instant startDate,
            @RequestParam("endDate") @DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME) Instant endDate
    ) {
        return ResponseEntity.ok(restaurantQueryService.getEventCountsByDateRange(restaurantId, startDate, endDate));
    }
}
