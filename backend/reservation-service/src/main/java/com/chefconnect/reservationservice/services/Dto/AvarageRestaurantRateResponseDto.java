package com.chefconnect.reservationservice.services.Dto;

public class AvarageRestaurantRateResponseDto {
    private int avarageRate;

    public AvarageRestaurantRateResponseDto(int avarageRate) {
        this.avarageRate = avarageRate;
    }

    public int getAvarageRate() {
        return avarageRate;
    }

    public void setAvarageRate(int avarageRate) {
        this.avarageRate = avarageRate;
    }
}
