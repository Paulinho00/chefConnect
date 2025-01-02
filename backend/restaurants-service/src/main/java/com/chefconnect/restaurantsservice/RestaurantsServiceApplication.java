package com.chefconnect.restaurantsservice;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;

@SpringBootApplication
@EnableConfigurationProperties(EventQueuesProperties.class)
public class RestaurantsServiceApplication {

    public static void main(String[] args) {
        SpringApplication.run(RestaurantsServiceApplication.class, args);
    }
}
