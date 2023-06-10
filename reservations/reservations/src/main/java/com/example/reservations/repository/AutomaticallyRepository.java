package com.example.reservations.repository;

import com.example.reservations.model.Automatically;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface AutomaticallyRepository extends JpaRepository<Automatically, Long> {
}
