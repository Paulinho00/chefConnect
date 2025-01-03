package com.chefconnect.reservationservice.models;

import java.time.LocalDateTime;
import java.util.HashSet;
import java.util.Set;
import java.util.UUID;

import jakarta.persistence.CascadeType;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.JoinTable;
import jakarta.persistence.ManyToMany;
import jakarta.persistence.OneToOne;
import jakarta.persistence.Table;
import lombok.Getter;
import lombok.Setter;

@Entity
@Table(name = "Reservation")
@Getter
@Setter
public class Reservation {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private UUID id;

    @Column(name = "date", nullable = false)
    private LocalDateTime date;

    @Column(name = "isApproved", nullable = false)
    private boolean isApproved;

    @Column(name = "numberOfPeople", nullable = false)
    private int numberOfPeople;

    @OneToOne(mappedBy = "reservation", optional = true, cascade = CascadeType.ALL)
    private CustomerOpinion customerOpinion;

    @ManyToMany
    @JoinTable(
        name = "reservation_table",
        joinColumns = @JoinColumn(name = "reservation_id"),
        inverseJoinColumns = @JoinColumn(name = "table_id")
    )
    private Set<TableReservation> tableReservations = new HashSet<>();;

    @Column(name = "userId", nullable = false)
    private UUID userId;

    @Column(name = "approvingWorkerId")
    private UUID approvingWorkerId;

    @Column(name = "restaurantId", nullable = false)
    private UUID restaurantId;

    @Column(name = "isDeleted", nullable = false)
    private boolean isDeleted;

    public Reservation(){
        this.isDeleted = false;
        this.isApproved = false;
    }
}