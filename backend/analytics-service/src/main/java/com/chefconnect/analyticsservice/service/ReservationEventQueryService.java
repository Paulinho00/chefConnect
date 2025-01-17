package com.chefconnect.analyticsservice.service;

import com.chefconnect.analyticsservice.repository.ReservationEventRepository;
import com.chefconnect.analyticsservice.service.dto.EventCountDto;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.Instant;
import java.util.List;
import java.util.UUID;

@Service
@RequiredArgsConstructor(onConstructor_ = @Autowired, access = lombok.AccessLevel.PROTECTED)
public class ReservationEventQueryService {

    private final ReservationEventRepository reservationEventRepository;

    public List<EventCountDto> getEventCountsByDateRange(UUID restaurantId, Instant startDate, Instant endDate) {
        return reservationEventRepository.countEventsByDateRangeAndRestaurant(restaurantId, startDate, endDate);
    }
}
