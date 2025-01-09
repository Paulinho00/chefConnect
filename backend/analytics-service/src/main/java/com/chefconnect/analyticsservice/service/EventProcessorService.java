package com.chefconnect.analyticsservice.service;

import com.chefconnect.analyticsservice.domain.ReservationEvent;
import com.chefconnect.analyticsservice.domain.Restaurant;
import com.chefconnect.analyticsservice.repository.ReservationEventRepository;
import com.chefconnect.analyticsservice.repository.RestaurantRepository;
import com.chefconnect.analyticsservice.service.dto.ReservationEventDto;
import io.awspring.cloud.sqs.annotation.SqsListener;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor(onConstructor_ = @Autowired, access = lombok.AccessLevel.PROTECTED)
public class EventProcessorService {

    private final ReservationEventRepository reservationEventRepository;
    private final RestaurantRepository restaurantRepository;

    @SqsListener("${events.queues.event-queue}")
    public void processReservationEvent(ReservationEventDto reservationEventDto) {
        var restaurantDto = reservationEventDto.restaurant();
        var restaurant = restaurantRepository.findById(restaurantDto.id())
                .orElseGet(() -> new Restaurant(restaurantDto.id(), restaurantDto.name(), restaurantDto.address()));

        var reservationEvent = new ReservationEvent(
                reservationEventDto.date(),
                restaurant,
                reservationEventDto.numberOfPeople()
        );
        reservationEventRepository.save(reservationEvent);
    }
}
