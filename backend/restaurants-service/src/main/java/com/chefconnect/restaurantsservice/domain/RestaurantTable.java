package com.chefconnect.restaurantsservice.domain;

import jakarta.persistence.*;
import lombok.Data;
import org.hibernate.annotations.UuidGenerator;

import java.util.UUID;

@Data
@Entity
@Table
public class RestaurantTable {

    @Id
    @UuidGenerator
    private UUID id;

    private int numberOfSeats;

    @ManyToOne(optional = false)
    @JoinColumn(name = "restaurant_id")
    private Restaurant restaurant;
}
