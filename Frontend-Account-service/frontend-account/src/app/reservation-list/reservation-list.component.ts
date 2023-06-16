import { Component, OnInit } from '@angular/core';
import { Reservation } from '../model/reservation';
import { ReservationService } from '../service/reservation.service';

@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrls: ['./reservation-list.component.css']
})
export class ReservationListComponent implements OnInit {
  reservations: Reservation[]=[];

  constructor(private reservationService: ReservationService) { }

  ngOnInit() {
    this.reservationService.getAll()
      .subscribe(reservations => this.reservations = reservations);
  }
}