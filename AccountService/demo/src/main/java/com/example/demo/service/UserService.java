package com.example.demo.service;

//import BloodBankAPI.GetAllByGuestIdRequest;
//import BloodBankAPI.GetAllByGuestIdResponse;
//import BloodBankAPI.GetByIdResponse;
//import BloodBankAPI.ReservationOuterClass;
import BloodBankAPI.*;
import BloodBankAPI.ReservationServiceGrpc.ReservationServiceImplBase;
import BloodBankLibrary.Core.Booking.Booking;
import BloodBankLibrary.Core.Booking.BookingServiceGrpc;
import BloodBankLibrary.Core.Booking.GetAllResponse;
import BloodBankLibrary.Core.Booking.GetByIdRequest;
import com.example.demo.dto.RateDTO;
import com.example.demo.dto.RateForAccDto;
import com.example.demo.model.*;
import com.example.demo.repository.HostRepository;
import com.example.demo.repository.RateForAccRepository;
import com.example.demo.repository.RateRepository;
import com.example.demo.repository.UserAppRepository;
import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDate;
import java.util.List;
import java.util.Optional;

@Service
public class UserService {


    private final ReservationServiceGrpc.ReservationServiceBlockingStub reservationServiceStub;

    private final BookingServiceGrpc.BookingServiceBlockingStub bookingServiceBlockingStub;
    private final UserAppRepository userRepository;

    @Autowired
    private RateRepository rateRepository;

    @Autowired
    private RateForAccRepository rateForAccRepository;

    @Autowired
    private HostRepository hostRepository;

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

    @Transactional
    public void deleteUser(Long userId){

        Optional<UserApp> userAppOptional = userRepository.findById(userId);
        if(userAppOptional.isPresent()) {

           // UserApp user = userAppOptional.get();
        UserApp user = userRepository.getById(userId);

        if (user.getRole().equals("GOST")) {
            GetAllByGuestIdRequest request = GetAllByGuestIdRequest.newBuilder()
                    .setGuestId(Math.toIntExact(userId))
                    .build();

            GetAllByGuestIdResponse response = reservationServiceStub.getAllByGuestId(request);

            if (response.getReservationsList().isEmpty()) {
                this.userRepository.deleteById(userId);
                System.out.println("Nalog je uspešno obrisan.");
            } else {
                // Ima aktivnih rezervacija, ne možemo obrisati nalog
                System.out.println("Nalog ne može biti obrisan jer postoje aktivne rezervacije ili nisi ukljucio drugi server.");
            }
        } else if (user.getRole().equals("HOST")) {
            GetByIdRequest request1 = GetByIdRequest.newBuilder()
                    .setId(Math.toIntExact(userId))
                    .build();
            GetAllResponse response1 = bookingServiceBlockingStub.getByHostId(request1);
            System.out.println("REZULTAT response1 ");
            System.out.println(response1);
            if (response1.getBookingsList().isEmpty()) {
                this.userRepository.deleteById(userId);
            }

        } else {
            System.out.println(user.getId());
            System.out.println(user.getRole());
            System.out.println("Nalog ne može biti obrisan jer korisnik nema ulogu hosta");
        }
       }



   }

    //ocenjivanje hosta
    public void rateHost(Long hostId, RateDTO rateDTO){
        UserApp loggedInUser = userRepository.findByUsername(SecurityContextHolder.getContext().getAuthentication().getName());
        if (loggedInUser.getRole().getName().equals("GOST")) {

               GetAllByGuestIdRequest request = GetAllByGuestIdRequest.newBuilder()
                        .setGuestId(Math.toIntExact(loggedInUser.getId()))
                        .build();

               GetAllAccommodationResponse response = reservationServiceStub.getAllGuestAccommodations(request);

                if(response.getAccommodationsList().isEmpty()){
                    System.out.println("ne moze da se doda ocena");
                } else {

                    Optional<Host> hostOptional = hostRepository.findById(hostId);
                    if (hostOptional.isPresent()) {
                        Host host = hostOptional.get();
                        List<Accomodation> accommodationsList = response.getAccommodationsList();
                        for (Accomodation accommodation : accommodationsList) {
                            if (accommodation.getHostId() == hostId) {
                                Rate rate = new Rate();
                                rate.setHost(host);
                                rate.setRate(rateDTO.getRate());
                                rate.setDate(LocalDate.now());
                                rateRepository.save(rate);
                            }
                        }
                    } else {
                        // Handle case when the host is not found
                    }



                }

        }
    }

    public void rateHost(RateDTO rateDTO){
        UserApp loggedInUser = userRepository.findByUsername(SecurityContextHolder.getContext().getAuthentication().getName());
        if (loggedInUser.getRole().getName().equals("GOST")) {
            Optional<Host>hostOptional = hostRepository.findById(rateDTO.getHostId());
            if(hostOptional.isPresent()){

               GetAllByGuestIdRequest request = GetAllByGuestIdRequest.newBuilder()
                        .setGuestId(Math.toIntExact(loggedInUser.getId()))
                        .build();

              GetAllAccommodationResponse response = reservationServiceStub.getAllGuestAccommodations(request);

                if(response.getAccommodationsList().isEmpty()){
                    System.out.println("ne moze da se doda ocena");
                } else {

                    if(response.getAccommodationsList().contains(hostOptional.get().getId())){
                        Rate rate = new Rate();
                        rate.setHost(hostOptional.get());
                        rate.setRate(rateDTO.getRate());
                        rate.setDate(LocalDate.now());
                        rateRepository.save(rate);
                    }



                }

            }


        }
    }

    public void updateRate(Long rateId, RateDTO rateDTO){
        Optional<Rate>rateOptional = rateRepository.findById(rateId);
        if(rateOptional.isPresent()){
            Rate rate = rateOptional.get();
            rate.setRate(rateDTO.getRate());
            rate.setDate(LocalDate.now());
            rateRepository.save(rate);
        }
    }
    public void deleteRate(Long rateId){
        Optional<Rate>rateOptional = rateRepository.findById(rateId);
        if(rateOptional.isPresent()){
            rateRepository.deleteById(rateId);
        }
    }

    public void getByIdRate(Long rateId){
        Optional<Rate>rateOptional = rateRepository.findById(rateId);
    }

    //srednja ocena
    public void averageRate() {
        List<Rate> rateList = rateRepository.findAll();
        double sum = 0.0;
        int count = 0;

        for (Rate rate : rateList) {
            sum += rate.getRate();
            count++;
        }

        if (count > 0) {
            double average = sum / count;
            System.out.println("Srednja ocena: " + average);
        } else {
            System.out.println("Nema dostupnih ocena.");
        }
    }


    //ocenjivanje smestaja
    public void rateAccommodation(Long id, RateForAccDto rateDTO){
        UserApp loggedInUser = userRepository.findByUsername(SecurityContextHolder.getContext().getAuthentication().getName());
        if (loggedInUser.getRole().getName().equals("GOST")) {
            //dodajemo ovde docine provere...tj metode a zsada i kada dohvatimo smestaj


            RateForAccommodation rateForAccomodation = new RateForAccommodation();
            rateForAccomodation.setRate(rateDTO.getRate());
            rateForAccomodation.setDate(LocalDate.now());
            rateForAccRepository.save(rateForAccomodation);

        }
    }

    public void updateRateAcc(Long rateId, RateForAccDto rateDTO){
        Optional<RateForAccommodation>rateOptional = rateForAccRepository.findById(rateId);
        if(rateOptional.isPresent()){
            RateForAccommodation rate = rateOptional.get();
            rate.setRate(rateDTO.getRate());
            rate.setDate(LocalDate.now());
            rateForAccRepository.save(rate);
        }
    }
    public void deleteRateForAcc(Long rateId){
        Optional<RateForAccommodation>rateOptional = rateForAccRepository.findById(rateId);
        if(rateOptional.isPresent()){
            rateForAccRepository.deleteById(rateId);
        }
    }

    public void getByIdRateAcc(Long rateId){
        Optional<RateForAccommodation>rateOptional = rateForAccRepository.findById(rateId);
        System.out.println("Rate Optional" + rateOptional.get());
    }

    //srednja ocena
    public void averageRateAcc() {
        List<RateForAccommodation> rateList = rateForAccRepository.findAll();
        double sum = 0.0;
        int count = 0;

        for (RateForAccommodation rate : rateList) {
            sum += rate.getRate();
            count++;
        }

        if (count > 0) {
            double average = sum / count;
            System.out.println("Srednja ocena: " + average);
        } else {
            System.out.println("Nema dostupnih ocena.");
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