package com.chefconnect.reservationservice.domain;

import java.util.HashSet;
import java.util.Set;
import java.util.UUID;

import com.chefconnect.reservationservice.services.Dto.RestaurantServicesDto.TableDto;
import com.fasterxml.jackson.annotation.JsonIgnore;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.ManyToMany;
import jakarta.persistence.Table;
import lombok.Getter;
import lombok.Setter;

@Entity
@Table(name = "TableReservation")
@Getter
@Setter
public class TableReservation {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private UUID id;

    @JsonIgnore
    @ManyToMany(mappedBy = "tableReservations")
    private Set<Reservation> reservations = new HashSet<>();

    @Column(name = "tableId", nullable = false)
    private UUID tableId;

    @Column(name = "restaurantId", nullable = false)
    private UUID restaurantId;

    @Column(name = "isDeleted", nullable = false)
    private boolean isDeleted;

    public TableReservation(){
        this.isDeleted = false;
    }

    public TableReservation(UUID tableId, UUID restaurantId) {
        this.tableId = tableId;
        this.restaurantId = restaurantId;
        this.isDeleted = false;
    }

    public static TableReservation fromTableDto(TableDto tableDto, UUID restaurantId) {
        return new TableReservation(tableDto.getId(), restaurantId);
    }
}
