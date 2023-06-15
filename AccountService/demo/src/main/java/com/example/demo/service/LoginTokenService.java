package com.example.demo.service;


import com.example.demo.model.LoginToken;
import com.example.demo.model.UserApp;
import com.example.demo.repository.LoginTokenRepository;
import com.example.demo.security.HashingAlogorithm;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.UUID;

@Service
public class LoginTokenService {
    @Autowired
    private LoginTokenRepository loginTokenRepository;

    public UUID generateLoginToken(UserApp loginUser){
        UUID token = UUID.randomUUID();
        LoginToken loginToken = LoginToken.builder()
                .id(UUID.randomUUID())
                .hmacValue(HashingAlogorithm.calculateHmac(token.toString()))
                .userApp(loginUser)
                .dateTimeStart(LocalDateTime.now())
                .dateTimeEnd(LocalDateTime.now().plusMinutes(10))
                .activated(false)
                .build();
        loginTokenRepository.save(loginToken);
        return token;
    }

    public UserApp validateTokenAndGetUser(UUID token){
        LoginToken loginToken = loginTokenRepository.findByHmacValue(HashingAlogorithm.calculateHmac(token.toString()));
        if(loginToken == null || loginToken.getActivated() || loginToken.isExpired()){
            return null;
        }
        loginToken.setActivated(true);
        loginTokenRepository.save(loginToken);
        return loginToken.getUserApp();
    }


}