package com.example.demo.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonIncludeProperties;
import lombok.*;
import org.apache.tomcat.jni.Local;

import javax.persistence.*;
import java.time.LocalDate;

@Entity
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
@Inheritance(strategy = InheritanceType.JOINED)
@JsonIgnoreProperties({"accountNonExpired", "credentialsNonExpired", "accountNonLocked", "authorities"})
public class Rate {

    @Id
    @SequenceGenerator(name = "rateSeqGen", sequenceName = "rateSeqGen", initialValue = 1, allocationSize = 1)
    @GeneratedValue(strategy = GenerationType.IDENTITY, generator = "rateSeqGen")
    @Column(name="id", unique=true, nullable=false)
    private long id;

    @ManyToOne(fetch = FetchType.EAGER)
    @JsonIncludeProperties({"id", "name"})
    private Host host;

    private int rate;

    private LocalDate date;


}