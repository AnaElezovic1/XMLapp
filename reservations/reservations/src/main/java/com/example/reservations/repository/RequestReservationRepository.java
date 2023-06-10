package com.example.reservations.repository;

import com.example.reservations.model.RequestReservation;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.util.List;

public interface RequestReservationRepository extends JpaRepository<RequestReservation, Long> {

    @Query("SELECT r FROM RequestReservation r WHERE (r.startTime <= :endTime AND r.endTime >= :startTime) AND r.id <> :requestId")
    List<RequestReservation> findOverlappingRequests(
            @Param("startTime") String startTime,
            @Param("endTime") String endTime,
            @Param("requestId") Long requestId
    );
}
