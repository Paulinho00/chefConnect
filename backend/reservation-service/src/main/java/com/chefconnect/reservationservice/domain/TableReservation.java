package com.chefconnect.reservationservice.domain;

import java.util.HashSet;
import java.util.Set;
import java.util.UUID;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.ManyToMany;
import jakarta.persistence.Table;

@Entity
@Table(name = "TableReservation")
public class TableReservation {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private UUID id;

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
}
