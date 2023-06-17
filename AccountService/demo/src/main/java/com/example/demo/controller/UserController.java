package com.example.demo.controller;

import com.example.demo.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/users")
public class UserController {

    @Autowired
    private UserService userService;

//    @GetMapping("/all")
//    public List<UserApp> getUsers(){
//        return userService.getUsers();
//    }
//
//    //@PostMapping("/add")
//    //public void addUser(@RequestBody UserApp users){
//    //  userService.addNewUser(users);
//    //}
//
//    @PostMapping("/add")
//    public UserApp addUser(@RequestBody userRegdto userRegistrationDTO) {
//        UserApp user = new UserApp();
//        user.setUsername(userRegistrationDTO.getUsername());
//        user.setPassword(userRegistrationDTO.getPassword());
//        user.setEmail(userRegistrationDTO.getEmail());
//        user.setRole(userRegistrationDTO.getRole());
//        user.setAdress(userRegistrationDTO.getAdress());
//
//        userService.addNewUser(user);
//        return user;
//    }
//
//    @PutMapping("/{id}")
//    public UserApp updateUser(@PathVariable("id") Long id, @RequestBody UserApp updatedUser) {
//        updatedUser.setId(id);
//        return userService.updateUser(updatedUser);
//    }


    @DeleteMapping(path="/delete/{userId}")
    public void deleteUser(@PathVariable("userId")Long userId){
        userService.deleteUser(userId);
    }

}
