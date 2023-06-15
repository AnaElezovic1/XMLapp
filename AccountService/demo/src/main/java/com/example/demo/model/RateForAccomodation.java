package com.example.demo.model;

import lombok.*;

import javax.persistence.*;
import java.time.LocalDate;

@Entity
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
@Inheritance(strategy = InheritanceType.JOINED)
public class RateForAccomodation {

    @Id
    @SequenceGenerator(name = "rateAccSeqGen", sequenceName = "rateAccSeqGen", initialValue = 1, allocationSize = 1)
    @GeneratedValue(strategy = GenerationType.IDENTITY, generator = "rateAccSeqGen")
    @Column(name="id", unique=true, nullable=false)
    private long id;

    private LocalDate date;

    private int rate;

}
