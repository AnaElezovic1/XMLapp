import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Users } from '../model/user';
import { UsersRegistrationDTO } from '../model/userRegistrationDTO';
import { userLoginDTO } from '../model/userLoginDTO';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = 'http://localhost:8082/users';

  constructor(private http: HttpClient) {}

  addUser(user: UsersRegistrationDTO): Observable<UsersRegistrationDTO> {
    return this.http.post<UsersRegistrationDTO>(`${this.baseUrl}/add`, user);
  }

  login(user: userLoginDTO): Observable<Users> {
    return this.http.post<Users>(`${this.baseUrl}/login`, user);
  }

  getUsers(): Observable<Users[]> {
    return this.http.get<Users[]>('http://localhost:8082/users/all');
  }

  updateUser(user: Users): Observable<Users> {
    return this.http.put<Users>(`${this.baseUrl}/${user.id}`, user);
  }
  deleteUser(userId: any){
    return this.http.delete<Users>('http://localhost:8082/users/'+userId );
  }
}
