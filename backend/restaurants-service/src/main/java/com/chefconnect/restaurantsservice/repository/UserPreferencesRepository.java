package com.chefconnect.restaurantsservice.repository;

import com.chefconnect.restaurantsservice.domain.UserPreferences;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface UserPreferencesRepository extends JpaRepository<UserPreferences, UUID> {
}
