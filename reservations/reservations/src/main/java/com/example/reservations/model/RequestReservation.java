package com.example.reservations.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonIncludeProperties;
import jakarta.persistence.*;
import lombok.*;

@Entity
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
@Inheritance(strategy = InheritanceType.JOINED)
public class RequestReservation {


    @Id
    @SequenceGenerator(name = "reservationSeqGen", sequenceName = "reservationSeqGen", initialValue = 1, allocationSize = 1)
    @GeneratedValue(strategy = GenerationType.IDENTITY, generator = "reservationSeqGen")
    @Column(name="id", unique=true, nullable=false)
    private Long id;

    private String startTime;

    private String endTime;

    private int numberOfGuest;

    private boolean approve;

    private int decline;



    @ManyToOne
    @JsonIncludeProperties({"id", "name"})
    private Accommodation accommodation;

    @OneToOne(mappedBy = "requestReservation", fetch = FetchType.EAGER)
    @JsonIgnoreProperties("requestReservation")
    private Reservation reservation;




}
