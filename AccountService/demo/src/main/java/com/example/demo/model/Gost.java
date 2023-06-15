package com.example.demo.model;


import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
@Getter
@Setter
@NoArgsConstructor
public class Gost extends UserApp {

    public Gost(UserApp userApp) {
        super(userApp.getId(), userApp.getUsername(), userApp.getEmail(), userApp.getPassword(), userApp.getPasswordSalt(), userApp.getAddress(), userApp.getActive(), userApp.getRole());
    }
}
