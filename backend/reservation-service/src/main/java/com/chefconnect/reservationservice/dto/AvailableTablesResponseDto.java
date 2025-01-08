package com.chefconnect.reservationservice.Dto;

public class AvailableTablesResponseDto {

    private String timeSpan;
    private int value;

    public AvailableTablesResponseDto() {
    }

    public String getTimeSpan() {
        return timeSpan;
    }

    public void setTimeSpan(String timeSpan) {
        this.timeSpan = timeSpan;
    }

    public int getValue() {
        return value;
    }

    public void setValue(int value) {
        this.value = value;
    }
}
