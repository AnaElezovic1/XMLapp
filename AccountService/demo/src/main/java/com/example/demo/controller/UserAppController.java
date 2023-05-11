package com.example.demo.controller;

import com.example.demo.model.UserApp;
import com.example.demo.dto.userRegdto;
import com.example.demo.dto.userLogdto;
import com.example.demo.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@CrossOrigin(origins= "http://localhost:4200/")
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

    //@PostMapping("/add")
    //public void addUser(@RequestBody UserApp users){
      //  userService.addNewUser(users);
    //}

    @PostMapping("/add")
    public UserApp addUser(@RequestBody userRegdto userRegistrationDTO) {
        UserApp user = new UserApp();
        user.setUsername(userRegistrationDTO.getUsername());
        user.setPassword(userRegistrationDTO.getPassword());
        user.setEmail(userRegistrationDTO.getEmail());
        user.setRole(userRegistrationDTO.getRole());
        user.setAdress(userRegistrationDTO.getAdress());

        userService.addNewUser(user);
        return user;
    }

    @PutMapping("/{id}")
    public UserApp updateUser(@PathVariable("id") Long id, @RequestBody UserApp updatedUser) {
        updatedUser.setId(id);
        return userService.updateUser(updatedUser);
    }


    @DeleteMapping(path="/delete/{userId}")
    public void deleteUser(@PathVariable("userId")Long userId){
        userService.deleteUser(userId);
    }

    @PostMapping("/login")
    public ResponseEntity<UserApp> login(@RequestBody userLogdto users) {
        UserApp loggedUser = new UserApp();
        loggedUser.setUsername(users.getUsername());
        loggedUser.setPassword(users.getPassword());

        UserApp user = userService.login(loggedUser.getUsername(), loggedUser.getPassword());
        if (user == null) {
            return null;
        }
        return ResponseEntity.ok(user);
    }

}
