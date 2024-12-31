package com.chefconnect.restaurantsservice.domain;

import jakarta.persistence.*;
import lombok.Data;
import org.hibernate.annotations.UuidGenerator;

import java.util.Collection;
import java.util.UUID;

@Data
@Entity
@Table
public class Restaurant {

    @Id
    @UuidGenerator
    private UUID id;

    private int numberOfTables;

    @OneToMany(mappedBy = "restaurant", cascade = CascadeType.ALL, orphanRemoval = true)
    private Collection<RestaurantTable> tables;

    @ManyToOne(optional = false)
    @JoinColumn(name="address_id")
    private Address address;

    private String name;
    private String openTime;
    private String closeTime;
}
