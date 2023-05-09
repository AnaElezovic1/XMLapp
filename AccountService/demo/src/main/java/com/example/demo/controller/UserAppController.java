package com.example.demo.controller;

import com.example.demo.model.UserApp;
import com.example.demo.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping(path="/users")
public class UserAppController {
    private  final UserService userService;

    @Autowired
    public UserAppController(UserService userService){
        this.userService=userService;
    }

    @GetMapping("/all")
    public List<UserApp> getUsers(){
        return userService.getUsers();
    }

    @PostMapping("/add")
    public void addUser(@RequestBody UserApp users){
        userService.addNewUser(users);
    }

    @DeleteMapping(path="/delete/{userId}")
    public void deleteUser(@PathVariable("userId")Long userId){
        userService.deleteUser(userId);
    }

    @PostMapping("/login")
    public ResponseEntity<UserApp> login(@RequestBody UserApp users) {
        UserApp user = userService.login(users.getUsername(), users.getPassword());
        if (user == null) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).build();
        }
        return ResponseEntity.ok(user);
    }

}
