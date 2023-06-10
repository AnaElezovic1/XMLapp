package com.example.reservations.controller;

import com.example.reservations.dto.ReservationDto;
import com.example.reservations.model.Automatically;
import com.example.reservations.model.RequestReservation;
import com.example.reservations.service.ReservationService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping(path ="/reservation")
@CrossOrigin(origins= "http://localhost:4200/")
public class ReservationController {

    @Autowired
    private ReservationService reservationService;

    @PostMapping("/addRequest/{automaticallyId}")
    public void  createReservation(@RequestBody ReservationDto reservationDto, @PathVariable Long automaticallyId){
        reservationService.createRequestReservation(reservationDto, automaticallyId);
    }

    @PostMapping("/{id}/addReservation")
    public void createReservation(@PathVariable Long id){
        reservationService.createReservation(id);
    }



    @DeleteMapping("/{id}/delete")
    public void deleteRequest(@PathVariable Long id){
        reservationService.deleteRequest(id);

    }


    @PutMapping("/{id}/cancelReservation/{hostId}")
    public void cancelReservation(@PathVariable Long id, @PathVariable Long hostId){
        reservationService.cancelReservation(id, hostId);

    }



    @PutMapping("/{id}/getAutomatically")
    public void getAutomatically(@PathVariable Long id){
        reservationService.updateAutomatically(id);
    }



    @GetMapping("/allRequestReservation")
    public List<RequestReservation>requestReservationList(){
        return reservationService.requestReservations();
    }

    @GetMapping("/{hostId}/declineReservation")
    public int declineReservation(@PathVariable Long hostId){
        return reservationService.declineReservation(hostId);
    }



    @PutMapping("{automaticallyId}/approveRequest/{requestId}")
    public void getManuallyApproveRequest(@PathVariable Long automaticallyId, @PathVariable Long requestId){
        reservationService.approveRequest(automaticallyId, requestId);
    }



    @PutMapping("{automaticallyId}/declineRequest/{requestId}")
    public void getManuallyDeclineRequest(@PathVariable Long automaticallyId, @PathVariable Long requestId){
        reservationService.declineRequest(automaticallyId, requestId);
    }



}
