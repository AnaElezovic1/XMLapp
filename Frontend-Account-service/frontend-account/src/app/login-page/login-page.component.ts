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
    const username = form.value.username;
    const password = form.value.password;

    this.authService.login(username,password).subscribe(
      (response) => {
        const role = this.authService.getRole();
        if (role === 'GOST') {
          this.router.navigate(['/user-profile']);
        }if(role === 'HOST')
        {
          this.router.navigate(['/user-profile']);
        }
        console.log('You have successfuly logged in!');
      },
      (error) => {
        console.log('Login failed:', error);
        // Show error message to user
      }
    );
  }
}
