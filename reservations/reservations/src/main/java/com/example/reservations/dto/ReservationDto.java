package com.example.reservations.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class ReservationDto {

    private String startTime;
    private String endTime;
    private int numberOfGuest;
    private Long accommodationId;
}
