package com.chefconnect.reservationservice;

import lombok.Getter;
import lombok.Setter;
import org.springframework.boot.context.properties.ConfigurationProperties;

@Getter
@Setter
@ConfigurationProperties(prefix = "events.queues")
public class EventQueuesProperties {

    private String eventQueue;
}
