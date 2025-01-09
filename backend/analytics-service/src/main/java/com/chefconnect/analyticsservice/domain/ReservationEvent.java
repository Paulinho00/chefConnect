package com.chefconnect.analyticsservice.domain;

import jakarta.persistence.Entity;
import jakarta.persistence.Table;
import lombok.AccessLevel;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.NoArgsConstructor;

import java.time.Instant;

@Data
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@EqualsAndHashCode(callSuper = true)
@Entity
@Table
public class ReservationEvent extends Event {

    private int numberOfPeople;

    public ReservationEvent(Instant date, Restaurant restaurant, int numberOfPeople) {
        super(date, restaurant);
        this.numberOfPeople = numberOfPeople;
    }
}
