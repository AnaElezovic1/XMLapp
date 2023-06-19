package com.example.demo.repository;

import com.example.demo.model.RateForAccommodation;
import org.springframework.data.jpa.repository.JpaRepository;

public interface RateForAccRepository extends JpaRepository<RateForAccommodation, Long> {
}