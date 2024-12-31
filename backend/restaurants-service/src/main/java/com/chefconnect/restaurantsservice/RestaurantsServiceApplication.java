package com.chefconnect.restaurantsservice;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.security.config.annotation.method.configuration.EnableMethodSecurity;

@SpringBootApplication
@EnableMethodSecurity
@EnableConfigurationProperties(EventQueuesProperties.class)
public class RestaurantsServiceApplication {

    public static void main(String[] args) {
        SpringApplication.run(RestaurantsServiceApplication.class, args);
    }

}
