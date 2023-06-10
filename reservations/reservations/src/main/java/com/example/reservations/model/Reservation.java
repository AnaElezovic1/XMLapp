package com.example.reservations.model;

import com.fasterxml.jackson.annotation.JsonIncludeProperties;
import jakarta.persistence.*;
import lombok.*;

@Entity
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class Reservation{

//    public Reservation(RequestReservation requestReservation) {
//        super(requestReservation.getId(), requestReservation.getStartTime(),requestReservation.getEndTime(),requestReservation.getNumberOfGuest(), requestReservation.isApprove(),requestReservation.getAccommodation());
//    }

    @Id
    @SequenceGenerator(name = "reservationSeqGen", sequenceName = "reservationSeqGen", initialValue = 1, allocationSize = 1)
    @GeneratedValue(strategy = GenerationType.IDENTITY, generator = "reservationSeqGen")
    @Column(name="id", unique=true, nullable=false)
    private Long id;

    @OneToOne
    @JsonIncludeProperties({"id", "name"})
    private RequestReservation requestReservation;

//    private int decline = 0;


}
