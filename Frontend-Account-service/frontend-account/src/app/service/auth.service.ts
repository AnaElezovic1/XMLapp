import { Injectable } from '@angular/core';
import { Users } from '../model/user';
import { UserService } from './user.service';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private loggedIn = false;
  loggedInUser!: Users; // using ! to assert that the variable will not be null

  get isLoggedIn(): boolean {
    return this.loggedIn;
  }

  // login() {
  //   this.loggedIn = true;
  // }

  // logout() {
  //   this.loggedIn = false;
  // }

  currentlyLoggedInUser(user: Users) {
    this.loggedInUser = user;
    return this.loggedInUser;
  }

  private loginUrl = 'http://localhost:8082/auth/login'; // ?

  constructor(private http: HttpClient, private router: Router) {}

  signup(registrationRequest: any): Observable<any> {
    const url = 'http://localhost:8082/auth/signup';
    return this.http.post(url, registrationRequest);
  }

  login(username: string, password: string): Observable<any> {
    return this.http.post<any>(this.loginUrl, { username, password }).pipe(
      tap((response) => {
        // Save the JWT token and expiration date in local storage
        console.log(response);
        console.log("auth.service1")
        localStorage.setItem('access_token', response.accessToken);
        localStorage.setItem('refresh_token', response.accessToken);
        console.log("auth.service2")
        const role = this.getRole(); // Get the role value
        console.log(`Ovo je rola nakon logovanja: ` + role); // Print the role value for testing
      })
    );
  }

  getRole() {
    const token = localStorage.getItem('access_token');
    if (token) {
      const base64Url = token!.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split('')
          .map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
          })
          .join('')
      );

      console.log("aaaa",jsonPayload)

      const payload = JSON.parse(jsonPayload);
      console.log("aaaabbbb",jsonPayload)
      return payload.role;
    }
    return null;
  }

  isUserLoggedIn() {
    const accessToken = localStorage.getItem('access_token');
    console.log("auth.service")
    return !!accessToken; // Returns true if access token exists, false otherwise
  }

  logout(): void {
    // Remove the JWT token and expiration date from local storage
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('expires_at');

    console.log(this.getRole());
    console.log(`uspesno izlogovan`);
    this.router.navigate(['/login-email']);
  }

}
