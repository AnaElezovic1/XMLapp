package com.example.demo.service;

import com.example.demo.model.UserApp;
import com.example.demo.repository.UserAppRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
@Service
public class UserService {

    private final UserAppRepository userRepository;

    @Autowired
    public UserService(UserAppRepository userRepository) {
        this.userRepository = userRepository;
    }

    public void addNewUser(UserApp users){
        userRepository.save(users);
    }

    public List<UserApp> getUsers(){
        return userRepository.findAll();
    }

    public void deleteUser(Long userId){
        userRepository.deleteById(userId);
    }
}
