package com.chefconnect.restaurantsservice.domain;

import jakarta.annotation.Nullable;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.Data;
import org.hibernate.annotations.UuidGenerator;

import java.util.UUID;

@Data
@Entity
@Table
public class Address {

    @Id
    @UuidGenerator
    private UUID id;

    private String street;

    private Integer streetNumber;

    @Nullable
    private Integer flatNumber;

    private String postalCode;

    private String city;

    @Override
    public String toString() {
        return street + " " + streetNumber + (flatNumber != null ? "/" + flatNumber : "") + ", " + postalCode + " " + city;
    }
}
