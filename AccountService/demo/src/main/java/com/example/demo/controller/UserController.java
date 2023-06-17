package com.example.demo.controller;

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



}