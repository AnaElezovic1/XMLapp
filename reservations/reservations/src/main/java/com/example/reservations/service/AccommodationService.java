package com.example.reservations.service;

import com.example.reservations.dto.AccommodationDto;
import com.example.reservations.model.Accommodation;
import com.example.reservations.repository.AccommodationRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class AccommodationService {

    @Autowired
    private AccommodationRepository accommodationRepository;


    public Accommodation createAccommodation(AccommodationDto accommodationDto){

        Accommodation accommodation = new Accommodation();
        accommodation.setName(accommodationDto.getName());
        accommodation.setDescription(accommodationDto.getDescription());
        accommodation.setImage(accommodationDto.getImage());
        accommodation.setLocation(accommodationDto.getLocation());
        accommodation.setBeds(accommodationDto.getBeds());
        return accommodationRepository.save(accommodation);
    }


}
