package com.chefconnect.reservationservice.models;

import java.util.UUID;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.OneToOne;
import jakarta.persistence.Table;

@Entity
@Table(name = "Customer_Opinion")
public class CustomerOpinion {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private UUID id;

    @Column(name = "rate", nullable = false)
    private int rate;

    @Column(name = "description")
    private String description;

    @Column(name = "publishDate", nullable = false)
    private String publishDate;

    @OneToOne
    @JoinColumn(name = "id")
    private Reservation reservation;

    @Column(name = "isDeleted", nullable = false)
    private boolean isDeleted;

    public CustomerOpinion(){
        this.isDeleted = false;
    }
}
