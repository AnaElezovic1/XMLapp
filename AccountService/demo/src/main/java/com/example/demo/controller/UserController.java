package com.example.demo.controller;

import com.example.demo.dto.RateDTO;
import com.example.demo.dto.RateForAccDto;
import com.example.demo.model.UserApp;
import com.example.demo.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@CrossOrigin(origins= "http://localhost:4200/")
@RequestMapping(path="/users")
public class UserController {
    private  final UserService userService;

    @Autowired
    public UserController(UserService userService){
        this.userService=userService;
    }

    @GetMapping("/all")
    public List<UserApp> getUsers(){
        return userService.getUsers();
    }

    //@PostMapping("/add")
    //public void addUser(@RequestBody UserApp users){
    //  userService.addNewUser(users);
    //}


    @PutMapping("/{id}")
    public UserApp updateUser(@PathVariable("id") Long id, @RequestBody UserApp updatedUser) {
        updatedUser.setId(id);
        return userService.updateUser(updatedUser);
    }


    @DeleteMapping(path="/delete/{userId}")
    public void deleteUser(@PathVariable("userId")Long userId){
        userService.deleteUser(userId);
    }

    @PostMapping("/{hostId}/rateHost")
    public void rateHost(@PathVariable Long hostId, @RequestBody RateDTO rateDTO){
        userService.rateHost(hostId,rateDTO);
    }

    @PostMapping("/{id}/rateAcc")
    public void rateAcc(@PathVariable Long id, @RequestBody RateForAccDto rateDTO){
        userService.rateAccommodation(id,rateDTO);
    }

    @PutMapping("/{hostId}/update")
    public void updateRate(@PathVariable Long hostId, @RequestBody RateDTO rateDTO){
        userService.updateRate(hostId, rateDTO);
    }

    @PutMapping("/{rateId}/updateAcc")
    public void updateRate(@PathVariable Long rateId, @RequestBody RateForAccDto rateDTO){
        userService.updateRateAcc(rateId, rateDTO);
    }

    @DeleteMapping("/deleteRating/{rateId}")
    public void deleteRate(@PathVariable Long rateId){
        userService.deleteRate(rateId);
    }

    @DeleteMapping("/deleteAcc/{rateId}")
    public void deleteAcc(@PathVariable Long rateId){
        userService.deleteRateForAcc(rateId);
    }

    @GetMapping("/{rateId}/getRate")
    public void getRate(@PathVariable Long rateId){
        userService.getByIdRate(rateId);
    }

    @GetMapping("/{rateId}/getRateAcc")
    public void getRateAcc(@PathVariable Long rateId){
        userService.getByIdRateAcc(rateId);
    }

    @GetMapping("/avgRate")
    public void getAvgRate(){
        userService.averageRate();
    }
    @GetMapping("/avgRateAcc")
    public void getAvgRateAcc(){
        userService.averageRateAcc();
    }



}