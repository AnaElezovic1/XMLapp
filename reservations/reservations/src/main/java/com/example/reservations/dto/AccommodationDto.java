package com.example.reservations.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class AccommodationDto {

    private String name;

    private String description;

    private String image;

    private String location;

    private int beds;

}
