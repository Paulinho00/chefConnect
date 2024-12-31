package com.chefconnect.restaurantsservice.domain;

import jakarta.persistence.*;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.ArrayList;
import java.util.Collection;
import java.util.UUID;

@Data
@Entity
@Table
@NoArgsConstructor
public class UserPreferences {

    @Id
    private UUID id;

    @ManyToMany(fetch = FetchType.EAGER)
    @JoinTable(
            name = "user_favorite_restaurants",
            joinColumns = @JoinColumn(name = "user_preferences_id"),
            inverseJoinColumns = @JoinColumn(name = "restaurant_id")
    )
    private Collection<Restaurant> favoriteRestaurants;

    public UserPreferences(UUID id, Restaurant restaurant) {
        this.id = id;
        this.favoriteRestaurants = new ArrayList<>();
        this.favoriteRestaurants.add(restaurant);
    }
}
