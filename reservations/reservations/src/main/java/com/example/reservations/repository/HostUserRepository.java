package com.example.reservations.repository;

import com.example.reservations.model.HostUser;
import org.springframework.data.jpa.repository.JpaRepository;

public interface HostUserRepository extends JpaRepository<HostUser, Long> {
}
