package com.example.demo.service;

import BloodBankAPI.GetAllByGuestIdRequest;
import BloodBankAPI.GetAllByGuestIdResponse;
import BloodBankAPI.GetByIdResponse;
import BloodBankAPI.ReservationServiceGrpc;
import BloodBankLibrary.Core.Booking.Booking;
import BloodBankLibrary.Core.Booking.BookingServiceGrpc;
import BloodBankLibrary.Core.Booking.GetAllResponse;
import BloodBankLibrary.Core.Booking.GetByIdRequest;
import com.example.demo.model.Role;
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

    private final BookingServiceGrpc.BookingServiceBlockingStub bookingServiceBlockingStub;
    private final UserAppRepository userRepository;

    @Autowired
    public UserService(UserAppRepository userRepository) {
        this.userRepository = userRepository;
        ManagedChannel channel = ManagedChannelBuilder.forAddress("localhost", 4311).usePlaintext().build();
        this.reservationServiceStub = ReservationServiceGrpc.newBlockingStub(channel);
        ManagedChannel channel1 = ManagedChannelBuilder.forAddress("localhost",4111).usePlaintext().build();
        this.bookingServiceBlockingStub = BookingServiceGrpc.newBlockingStub(channel1);
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

        UserApp user = userRepository.getById(userId);

        if(user.getRole() == Role.GOST){
            GetAllByGuestIdRequest request = GetAllByGuestIdRequest.newBuilder()
                    .setGuestId(Math.toIntExact(userId))
                    .build();

            GetAllByGuestIdResponse response = reservationServiceStub.getAllByGuestId(request);

            if(response.getReservationsList().isEmpty()){
                this.userRepository.deleteById(userId);
                System.out.println("Nalog je uspešno obrisan.");
            } else {
                // Ima aktivnih rezervacija, ne možemo obrisati nalog
                System.out.println("Nalog ne može biti obrisan jer postoje aktivne rezervacije ili nisi ukljucio drugi server.");
            }
        } else if(user.getRole() == Role.HOST){
            GetByIdRequest request1 = GetByIdRequest.newBuilder()
                    .setId(Math.toIntExact(userId))
                    .build();
            GetAllResponse response1 = bookingServiceBlockingStub.getByHostId(request1);
            System.out.println("REZULTAT response1 ");
            System.out.println(response1);
            if(response1.getBookingsList().isEmpty()){
                this.userRepository.deleteById(userId);
            }

        } else {
            System.out.println(user.getId());
            System.out.println(user.getRole());
            System.out.println("Nalog ne može biti obrisan jer korisnik nema ulogu hosta");
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
