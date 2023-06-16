import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Accommodation } from '../model/accommodation';


@Injectable({
  providedIn: 'root',
})
export class AccommodationService {
  private baseUrl = 'https://localhost:5001/api/Accomodation';

  constructor(private http: HttpClient) {}

  addUser(accommodation: Accommodation): Observable<any> {
    return this.http.post<any>('https://localhost:5001/api/Accomodation', accommodation);
  }

  getAll(): Observable<any[]> {
    return this.http.get<any[]>('https://localhost:5001/api/Accomodation');
  }

}
