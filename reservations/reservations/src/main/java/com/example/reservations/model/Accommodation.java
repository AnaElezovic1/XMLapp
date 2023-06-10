package com.example.reservations.model;

import com.fasterxml.jackson.annotation.JsonIgnore;
import jakarta.persistence.*;
import lombok.*;

import java.util.List;

@Entity
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
@Inheritance(strategy = InheritanceType.JOINED)
public class Accommodation {

    @Id
    @Column(name="id", unique=true, nullable=false)
    private Long id;

    private String name;

    private String description;

    private String image;

    private String location;

    private int beds;

//    private boolean automaticApproval;

    @OneToMany(mappedBy = "accommodation")
    @JsonIgnore
    private List<RequestReservation> reservation;


}
