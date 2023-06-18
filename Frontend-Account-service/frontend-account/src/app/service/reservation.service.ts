import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Reservation } from '../model/reservation';


@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  private baseUrl = 'http://localhost:16377/api/Reservation';

  constructor(private http: HttpClient) {}

  addUser(accommodation: Reservation): Observable<any> {
    return this.http.post<any>('http://localhost:16377/api/Reservation', accommodation);
  }

  getAll(): Observable<any[]> {
    return this.http.get<any[]>('http://localhost:16377/api/Reservation');
  }
  getByGuest(id:number): Observable<any[]> {
    return this.http.get<any[]>('http://localhost:16377/api/Reservation/guest/'+id);
  }
  update(reservation:Reservation): Observable<any[]> {
    return this.http.post<any[]>('http://localhost:16377/api/Reservation/update/'+reservation.id,reservation);
  }
  delete(id:number): Observable<any[]> {
    return this.http.get<any[]>('http://localhost:16377/api/Reservation/delete/'+id.toString());
  }

}
