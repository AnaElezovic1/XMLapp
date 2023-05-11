import { Injectable } from '@angular/core';
import { Users } from '../model/user';
import { UserService } from './user.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private loggedIn = false;
  loggedInUser!: Users; // using ! to assert that the variable will not be null

  get isLoggedIn(): boolean {
    return this.loggedIn;
  }

  login() {
    this.loggedIn = true;
  }

  logout() {
    this.loggedIn = false;
  }

  currentlyLoggedInUser(user: Users) {
    this.loggedInUser = user;

    return this.loggedInUser;
  }
}
