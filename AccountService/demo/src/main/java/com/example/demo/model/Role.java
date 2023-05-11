package com.example.demo.model;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;



public enum Role {
    NEREGISTROVANI_KORISNIK("NK"),
    GOST("G"),
    HOST("H");


    private final String user;

    Role(String user) {
        this.user = user;
    }
}