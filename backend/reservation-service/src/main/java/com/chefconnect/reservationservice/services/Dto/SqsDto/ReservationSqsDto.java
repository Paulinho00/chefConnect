package com.chefconnect.reservationservice.services.Dto.SqsDto;

public class ReservationSqsDto {
    private String date;
    public String getDate() {
        return date;
    }
    public void setDate(String date) {
        this.date = date;
    }
    private RestaurantSqsDto restaurant;
    public RestaurantSqsDto getRestaurant() {
        return restaurant;
    }
    public void setRestaurant(RestaurantSqsDto restaurant) {
        this.restaurant = restaurant;
    }
    private int numberOfPeople;
    public int getNumberOfPeople() {
        return numberOfPeople;
    }
    public void setNumberOfPeople(int numberOfPeople) {
        this.numberOfPeople = numberOfPeople;
    }
}
