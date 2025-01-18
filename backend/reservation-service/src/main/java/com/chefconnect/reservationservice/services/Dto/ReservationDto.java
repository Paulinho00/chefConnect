package com.chefconnect.reservationservice.services.Dto;

import java.time.LocalDateTime;
import java.util.UUID;

import com.chefconnect.reservationservice.domain.ReservationStatus;

public class ReservationDto {

    private UUID id;
    private String address;
    private int numberOfTable;
    private LocalDateTime date;
    private ReservationStatus reservationStatus;
    private boolean isOpinion;

    public ReservationDto(UUID id, String address, int numberOfTable, LocalDateTime date, ReservationStatus reservationStatus, boolean isOpinion) {
        this.id = id;
        this.address = address;
        this.numberOfTable = numberOfTable;
        this.date = date;
        this.reservationStatus = reservationStatus;
        this.isOpinion = isOpinion;
    }

    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public int getNumberOfTable() {
        return numberOfTable;
    }

    public void setNumberOfTable(int numberOfTable) {
        this.numberOfTable = numberOfTable;
    }

    public LocalDateTime getDate() {
        return date;
    }

    public void setDate(LocalDateTime date) {
        this.date = date;
    }

    public ReservationStatus getReservationStatus() {
        return reservationStatus;
    }

    public void setStatus(ReservationStatus reservationStatus) {
        this.reservationStatus = reservationStatus;
    }

    public boolean isOpinion() {
        return isOpinion;
    }

    public void setOpinion(boolean isOpinion) {
        this.isOpinion = isOpinion;
    }
}
