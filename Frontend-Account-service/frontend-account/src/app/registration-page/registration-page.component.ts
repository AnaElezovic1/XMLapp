import { Component, OnInit } from '@angular/core';
import { UserService } from '../service/user.service';
import { NgForm } from '@angular/forms';
import { Users } from '../model/user';
import { UsersRegistrationDTO } from '../model/userRegistrationDTO';
import { Role } from '../model/role-enum';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.css'],
})
export class RegistrationPageComponent implements OnInit {
  constructor(private service: UserService) {}

  roles = Object.values(Role);
  selectedRole!: Role;

  ngOnInit(): void {}

  submitForm(form: NgForm) {
    if (form.valid) {
      const newUser: UsersRegistrationDTO = {
        username: form.value.username,
        password: form.value.password,
        email: form.value.email,
        adress: form.value.adress,
        role: this.selectedRole,
      };
      this.service.addUser(newUser).subscribe((response) => {
        console.log(response);
      });
    }
  }
}
