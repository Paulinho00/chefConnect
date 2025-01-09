package com.chefconnect.analyticsservice.domain;

import jakarta.persistence.*;
import lombok.AccessLevel;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.Instant;
import java.util.UUID;

@Data
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@Entity
@Inheritance(strategy = InheritanceType.TABLE_PER_CLASS)
@Table
public class Event {

    @Id
    @GeneratedValue
    private UUID id;

    private Instant date;

    @ManyToOne
    @JoinColumn(name = "restaurant_id", nullable = false)
    private Restaurant restaurant;

    public Event(Instant date, Restaurant restaurant) {
        this.date = date;
        this.restaurant = restaurant;
    }
}
