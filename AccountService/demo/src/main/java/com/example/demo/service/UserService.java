package com.example.demo.service;

import com.example.demo.model.UserApp;
import com.example.demo.repository.UserAppRepository;
import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import io.grpc.StatusRuntimeException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.demo.accomodation.*;

import java.util.List;
import java.util.Optional;

@Service
public class UserService {

    private final AccomodationServiceGrpc.AccomodationServiceBlockingStub accomodationServiceStub;

    private final UserAppRepository userRepository;

    @Autowired
    public UserService(UserAppRepository userRepository) {
        this.userRepository = userRepository;
        ManagedChannel channel = ManagedChannelBuilder.forAddress("localhost", 4111).usePlaintext().build();
        this.accomodationServiceStub = AccomodationServiceGrpc.newBlockingStub(channel);
    }

    public void createAccommodation(Accomodation accommodation) {
        // Pozivanje gRPC metoda Create iz AccomodationService
        Accomodation request = Accomodation.newBuilder()
                .setId(accommodation.getId())
                .setName(accommodation.getName())
                .setDescription(accommodation.getDescription())
                .setLocation(accommodation.getLocation())
                .setImages(accommodation.getImages())
                .setBeds(accommodation.getBeds())
                .build();

        try {
            accomodationServiceStub.create(request);
            System.out.println("Smeštaj je uspešno kreiran.");
        } catch (StatusRuntimeException e) {
            System.out.println("Greška prilikom kreiranja smeštaja: " + e.getStatus());
        }
    }

    public void addNewUser(UserApp users){
        UserApp existingUser = userRepository.findByUsername(users.getUsername());
        if(existingUser != null){
            throw new RuntimeException("Korisnik sa datim korisničkim imenom već postoji!");
        } else {
            userRepository.save(users);
        }
    }

    public List<UserApp> getUsers(){
        return userRepository.findAll();
    }

    public void deleteUser(Long userId){
        userRepository.deleteById(userId);
    }

    public UserApp login(String username, String password) {
        List<UserApp> users = userRepository.findUserByUsernameAndPassword(username, password);
        if (users.isEmpty()) {
            return null;
        }
        return users.get(0);
    }

    public UserApp updateUser(UserApp updatedUser) {
        Optional<UserApp> user = userRepository.findById(updatedUser.getId());

        if (user.isPresent()) {
            return userRepository.save(updatedUser);
        } else {
            // Handle the case when the user doesn't exist
            throw new  RuntimeException("Username not found!");
        }
    }


}
