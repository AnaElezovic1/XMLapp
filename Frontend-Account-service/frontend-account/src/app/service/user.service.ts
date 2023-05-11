import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Users } from '../model/user';
import { UsersRegistrationDTO } from '../model/userRegistrationDTO';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = 'http://localhost:8082/users';

  constructor(private http: HttpClient) {}

  addUser(user: UsersRegistrationDTO): Observable<UsersRegistrationDTO> {
    return this.http.post<UsersRegistrationDTO>(`${this.baseUrl}/add`, user);
  }
}
