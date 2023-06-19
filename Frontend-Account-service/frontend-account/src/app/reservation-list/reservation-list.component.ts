import { Component, OnInit } from '@angular/core';
import { Reservation } from '../model/reservation';
import { ReservationService } from '../service/reservation.service';
import { Users } from '../model/user';
import { AuthService } from '../service/auth.service';
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
  constructor(private authService:AuthService,private reservationService: ReservationService) { }

  ngOnInit() {
    //this.authService.currentlyLoggedInUser(this.user);
    if(this.authService.loggedInUser.role=="HOST"){
        this.isHost=true;
        this.isGuest=false;
    }
  
   // this.authService.currentlyLoggedInUser(this.user);
    if(this.authService.loggedInUser.role=="GUEST")
    {
     this.reservationService.getByGuest(this.authService.loggedInUser.id).subscribe(reservations => this.reservations = reservations); 
    }
    else{
    this.reservationService.getAll()
      .subscribe(reservations => this.reservations = reservations);
    }
  }
  delAcc(sid:Reservation)
  {
    console.log(sid);
    this.reservations.filter(e=>e.id!=sid.id);
    this.reservationService.delete(sid.id as number).subscribe();
    this.reservations.splice(this.reservations.indexOf(sid), 1);
  }
  upAcc(res:Reservation)
  {
    res.accepted=true;
    console.log(res);
    this.reservationService.update(res).subscribe();

  }
  refresh()
  {

  }
}