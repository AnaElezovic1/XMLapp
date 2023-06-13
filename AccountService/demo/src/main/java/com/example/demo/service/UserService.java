package com.example.demo.service;

import BloodBankAPI.GetAllByGuestIdRequest;
import BloodBankAPI.GetAllByGuestIdResponse;
import BloodBankAPI.ReservationServiceGrpc;
import com.example.demo.model.UserApp;
import com.example.demo.repository.UserAppRepository;
import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class UserService {


    private final ReservationServiceGrpc.ReservationServiceBlockingStub reservationServiceStub;
    private final UserAppRepository userRepository;

    @Autowired
    public UserService(UserAppRepository userRepository) {
        this.userRepository = userRepository;
        ManagedChannel channel = ManagedChannelBuilder.forAddress("localhost", 4311).usePlaintext().build();
        this.reservationServiceStub = ReservationServiceGrpc.newBlockingStub(channel);
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

        GetAllByGuestIdRequest request = GetAllByGuestIdRequest.newBuilder()
                .setGuestId(Math.toIntExact(userId))
                .build();

        GetAllByGuestIdResponse response = reservationServiceStub.getAllByGuestId(request);

        if(response.getReservationsList().isEmpty()){
            this.
                    userRepository.deleteById(userId);
            System.out.println("Nalog je uspešno obrisan.");
        } else {
            // Ima aktivnih rezervacija, ne možemo obrisati nalog
            System.out.println("Nalog ne može biti obrisan jer postoje aktivne rezervacije ili nisi ukljucio drugi server.");
        }


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
