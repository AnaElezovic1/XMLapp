package com.example.reservations.repository;

import com.example.reservations.model.Accommodation;
import com.example.reservations.model.RequestReservation;
import com.example.reservations.model.Reservation;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface ReservationRepository extends JpaRepository<Reservation, Long> {


}
