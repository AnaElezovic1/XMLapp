import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Booking } from '../model/booking';


@Injectable({
  providedIn: 'root',
})
export class BookingService {
  private baseUrl = 'http://localhost:16277/api/Booking';

  constructor(private http: HttpClient) {}

  addUser(accommodation: Booking): Observable<any> {
    return this.http.post<any>('http://localhost:16277/api/Booking', accommodation);
  }

  getAll(): Observable<any[]> {
    return this.http.get<any[]>('http://localhost:16277/api/Booking');
  }

}
