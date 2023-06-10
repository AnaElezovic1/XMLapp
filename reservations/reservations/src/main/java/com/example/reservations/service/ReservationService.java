package com.example.reservations.service;

import com.example.reservations.dto.ReservationDto;
import com.example.reservations.model.*;
import com.example.reservations.repository.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class ReservationService {

    @Autowired
    private ReservationRepository reservationRepository;
    @Autowired
    private AccommodationRepository accommodationRepository;

    @Autowired
    private RequestReservationRepository requestReservationRepository;
    @Autowired
    private AutomaticallyRepository automaticallyRepository;
    @Autowired
    private HostUserRepository hostUserRepository;


    public void createRequestReservation(ReservationDto reservationDto,Long automaticallyId){
        Optional<Accommodation>accommodationOptional = accommodationRepository.findById(reservationDto.getAccommodationId());

        List<RequestReservation>requestReservationList = requestReservationRepository.findAll();
        boolean requestExists = false;

        for (RequestReservation r:requestReservationList
             ) {
            if (r.getStartTime().equals(reservationDto.getStartTime()) && r.getEndTime().equals(reservationDto.getEndTime()) && r.getAccommodation().getId().equals(accommodationOptional.get().getId())) {
                if (r.isApprove()) {
                    System.out.println("you can not create request");
                    requestExists = true;
                    break;
                }
            }
        }
        if(!requestExists){
                RequestReservation requestReservation = new RequestReservation();
                requestReservation.setStartTime(reservationDto.getStartTime());
                requestReservation.setEndTime(reservationDto.getEndTime());
//                requestReservation.setApprove(false);
                requestReservation.setNumberOfGuest(reservationDto.getNumberOfGuest());
                requestReservation.setAccommodation(accommodationOptional.get());

                Optional<Automatically>automaticallyOptional = automaticallyRepository.findById(automaticallyId);
                Automatically automatically = automaticallyOptional.get();

                if (automatically.isAutomaticApproval()) {
                    requestReservation.setApprove(true);
                    Reservation reservation = new Reservation();
                    reservation.setRequestReservation(requestReservation);
                    requestReservationRepository.save(requestReservation);
                    reservationRepository.save(reservation);
                } else {
                    requestReservation.setApprove(false);
                    requestReservationRepository.save(requestReservation);
                }



            }

        }



    public void deleteRequest(Long id){
        Optional<RequestReservation>requestReservationOptional = requestReservationRepository.findById(id);
        if(requestReservationOptional.isPresent()){
            if(!requestReservationOptional.get().isApprove()){
                requestReservationRepository.delete(requestReservationOptional.get());
            }else{
                System.out.println("You can not delete request");
            }
        }
    }

    public void createReservation(Long id){
        Optional<RequestReservation>requestReservationOptional = requestReservationRepository.findById(id);
        if(requestReservationOptional.isPresent()){
            requestReservationOptional.get().setApprove(true);
            RequestReservation requestReservation = requestReservationOptional.get();
            requestReservationRepository.save(requestReservation);
            Reservation reservation = new Reservation();
            reservation.setRequestReservation(requestReservation);
            reservationRepository.save(reservation);

        }
    }


    //odbijanje rezervacije
    public void cancelReservation(Long id, Long hostId) {

        Optional<Reservation>reservationOptional = reservationRepository.findById(id);
        Reservation reservation = reservationOptional.get();
        LocalDate currentDate = LocalDate.now();
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd.MM.yyyy");
        LocalDate startDate = LocalDate.parse(reservation.getRequestReservation().getStartTime(), formatter);

        if (currentDate.isBefore(startDate.minusDays(1))) {
            reservation.getRequestReservation().setApprove(false);

            //samo sto bi meni ovde trebalo za korisnika da mi odradi
            Optional<HostUser>hostUserOptional = hostUserRepository.findById(hostId);
            int declineReservation = 0;
            declineReservation = hostUserOptional.get().getDeclineReservation() +1;
            hostUserOptional.get().setDeclineReservation(declineReservation);
            hostUserRepository.save(hostUserOptional.get());
            System.out.println("Reservation canceled successfully.");
        } else {
            System.out.println("Cannot cancel reservation. The cancellation period has passed.");
        }
    }



    public void updateAutomatically(Long id){
       Optional<Automatically>automatically = automaticallyRepository.findById(id);
       Automatically automatically1 = automatically.get();
       if(!automatically1.isAutomaticApproval()) {
           automatically1.setAutomaticApproval(true);
           automaticallyRepository.save(automatically1);
       }else{
           automatically1.setAutomaticApproval(false);
           automaticallyRepository.save(automatically1);
       }
//        automaticallyRepository.save(automatically);
    }


    public List<RequestReservation> requestReservations() {
        List<RequestReservation> requestReservationList = requestReservationRepository.findAll();
        List<RequestReservation> newRequestReservationList = new ArrayList<>();

        for (RequestReservation r : requestReservationList) {
            if (!r.isApprove()) {
                newRequestReservationList.add(r);
            }
        }

        return newRequestReservationList;
    }

    public int declineReservation(Long hostUser){
        Optional<HostUser>hostUserOptional = hostUserRepository.findById(hostUser);
        return hostUserOptional.get().getDeclineReservation();
    }




    //za ova dva proveriti jos

    //pitanje da li mi treba ovde metoda jer ja sam kada sam postavila da se kreira zahtev ja sam po defaultu stavila da je o false sad ne vidim poentu ove metode onda??
    public void approveRequest(Long automaticallyId, Long requestId) {
        Optional<Automatically> automaticallyOptional = automaticallyRepository.findById(automaticallyId);
        if (automaticallyOptional.isPresent()) {
            if (!automaticallyOptional.get().isAutomaticApproval()) {
                Optional<RequestReservation> requestReservationOptional = requestReservationRepository.findById(requestId);
                if (requestReservationOptional.isPresent()) {
                    RequestReservation approvedRequest = requestReservationOptional.get();
                    approvedRequest.setApprove(true);
                    Reservation reservation = new Reservation();
                    reservation.setRequestReservation(approvedRequest);
                    requestReservationRepository.save(approvedRequest);
                    reservationRepository.save(reservation);

                    List<RequestReservation> overlappingRequests = requestReservationRepository.findOverlappingRequests(
                            approvedRequest.getStartTime(), approvedRequest.getEndTime(), requestId);
                    for (RequestReservation overlappingRequest : overlappingRequests) {
                        overlappingRequest.setApprove(false);
                        requestReservationRepository.save(overlappingRequest);
                    }
                }
            }
        }
    }

    public void declineRequest(Long automaticallyId, Long requestId){
        Optional<Automatically> automaticallyOptional = automaticallyRepository.findById(automaticallyId);
        if (automaticallyOptional.isPresent()) {
            if (!automaticallyOptional.get().isAutomaticApproval()) {
                Optional<RequestReservation> requestReservationOptional = requestReservationRepository.findById(requestId);
                if (requestReservationOptional.isPresent()) {
                    RequestReservation approvedRequest = requestReservationOptional.get();
                    approvedRequest.setApprove(false);
                    requestReservationRepository.save(approvedRequest);
                }
            }
        }
    }



}
