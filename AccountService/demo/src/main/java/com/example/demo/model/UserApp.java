package com.example.demo.model;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;


import java.util.Arrays;
import java.util.Collection;

@Entity
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class UserApp  {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    @Column(nullable=false,updatable=false)
    private Long id;

    @Column(unique = true)
    private String username;

    //@JsonIgnore
    @Column
    private String password;

    @Column
    private String email;

    @Enumerated(EnumType.ORDINAL)
    private Role role;

    @Column
    private String adress;


}