package com.example.demo.controller;

import com.example.demo.dto.LoginRequestDto;
import com.example.demo.dto.RegistrationRequestDto;
import com.example.demo.service.AuthenticationService;
import com.example.demo.utils.TokenUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.util.UUID;

@RestController
@RequestMapping("/auth")
public class AuthenticationController {
    @Autowired
    private TokenUtils tokenUtils;

    @Autowired
    private AuthenticationManager authenticationManager;

    @Autowired
    private AuthenticationService authenticationService;

    @PostMapping("/signup")
    public ResponseEntity<?> signup(@Valid @RequestBody RegistrationRequestDto request) {
        try{
            return authenticationService.registerUser(request);
        }
        catch (Exception e){
            return new ResponseEntity<>(e.getLocalizedMessage(), HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @PostMapping("/login")
    public ResponseEntity<?> login(@Valid @RequestBody LoginRequestDto request) {
        try{
            return authenticationService.login(request);
        }
        catch (Exception e){
            return new ResponseEntity<>(e.getLocalizedMessage(), HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }



//    @PostMapping("/refreshToken")
//    public ResponseEntity<?> refreshToken(@Valid @RequestBody RefreshTokenRequestDto refreshToken){
//        try{
//            return authenticationService.refreshToken(refreshToken.getRefreshToken());
//        }
//        catch (Exception e){
//            return new ResponseEntity<>(e.getLocalizedMessage(), HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }
//
//    @GetMapping("/activate")
//    public ResponseEntity<?> activate(@RequestParam("token") UUID token){
//        try{
//            return authenticationService.activate(token);
//        }
//        catch (Exception e){
//            return new ResponseEntity<>(e.getLocalizedMessage(), HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }
//
//    @GetMapping("/loginViaEmail/{email}")
//    public ResponseEntity<?> loginViaEmail(@PathVariable String email){
//        try{
//            return authenticationService.sendLoginTokenToEmail(email);
//        }
//        catch (Exception e){
//            return new ResponseEntity<>(e.getLocalizedMessage(), HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }

//    @GetMapping("/login")
//    public ResponseEntity<?> loginViaToken(@RequestParam("token") UUID token)
//    {
//        try{
//            return authenticationService.loginViaToken(token);
//        }
//        catch (Exception e){
//            return new ResponseEntity<>(e.getLocalizedMessage(), HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }
}
