package com.chefconnect.analyticsservice.service.dto;

import java.time.Instant;

public record EventCountDto(Instant date, long count) { }
