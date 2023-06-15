package com.example.demo.repository;


import com.example.demo.model.LoginToken;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface LoginTokenRepository extends JpaRepository<LoginToken, UUID> {
    public LoginToken findByHmacValue(String hmacValue);

}

