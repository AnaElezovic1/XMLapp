package com.example.demo.model;


import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.OneToMany;
import java.util.List;

@Entity
@Getter
@Setter
@NoArgsConstructor
public class Host extends UserApp {

    @OneToMany(mappedBy = "host", fetch = FetchType.EAGER)
    @JsonIgnoreProperties("host")
    private List<Rate> rate;

    public Host(UserApp userApp) {
        super(userApp.getId(), userApp.getUsername(), userApp.getEmail(), userApp.getPassword(), userApp.getPasswordSalt(), userApp.getAddress(), userApp.getActive(), userApp.getRole());
    }
}
