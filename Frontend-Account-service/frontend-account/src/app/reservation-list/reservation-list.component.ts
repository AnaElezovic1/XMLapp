import { Component, OnInit } from '@angular/core';
import { Reservation } from '../model/reservation';
import { ReservationService } from '../service/reservation.service';
import { Users } from '../model/user';
import { AuthService } from '../service/auth.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrls: ['./reservation-list.component.css']
})
export class ReservationListComponent implements OnInit {
  reservations: Reservation[]=[];
  private user:Users={
    id:0,
    username:"user",
    password:"pass",
    email:"email",
    role:"H",
    adress:"nesto"
  }
  isHost:boolean=false;
  isGuest:boolean=true;
  constructor(private authService:AuthService,private reservationService: ReservationService,private location:Location) { }

  ngOnInit() {
    this.authService.currentlyLoggedInUser(this.user);
    if(this.authService.loggedInUser.role=="H"){
        this.isHost=true;
        this.isGuest=false;
    }
  
    this.authService.currentlyLoggedInUser(this.user);
    if(this.authService.loggedInUser.role="G")
    {
     this.reservationService.getByGuest(this.authService.loggedInUser.id).subscribe(reservations => this.reservations = reservations); 
    }
    else{
    this.reservationService.getAll()
      .subscribe(reservations => this.reservations = reservations);
    }
  }
  delAcc(sid:Number)
  {
    console.log(sid);
    this.reservationService.delete(sid as number).subscribe();
  }
  upAcc(res:Reservation)
  {
    res.accepted=true;
    console.log(res);
    this.reservationService.update(res).subscribe();

  }
}