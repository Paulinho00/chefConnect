package com.chefconnect.reservationservice.Dto;

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
