import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { userLoginDTO } from '../model/userLoginDTO';
import { Router } from '@angular/router';
import { UserService } from '../service/user.service';
import { Users } from '../model/user';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
})
export class LoginPageComponent implements OnInit {
  constructor(
    private router: Router,
    private userService: UserService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {}

  submitForm(form: NgForm) {
    if (form.valid) {
      const newUser: userLoginDTO = {
        username: form.value.username,
        password: form.value.password,
      };

      this.userService.login(newUser).subscribe(
        (user: Users) => {
          console.log(user);
          if (user) {
            this.authService.currentlyLoggedInUser(user);
            this.authService.login();
            this.router.navigate(['/user-profile']);
          } else {
            console.log('Invalid credentials');
          }
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }
}
