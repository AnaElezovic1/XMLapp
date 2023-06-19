import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { Users } from '../model/user';
import { UserService } from '../service/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css'],
})
export class UserProfileComponent implements OnInit {
  user!: Users;
  users!: Users[];
  loggedIn = false;

  constructor(
    private authService: AuthService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.userService.getUsers().subscribe((users) => {
      this.users = users;
      console.log(this.users);
      this.loggedIn = this.authService.isLoggedIn;
      this.user = this.authService.loggedInUser;
    });
  }

  onSubmit(): void {
    this.userService.updateUser(this.user).subscribe(
      (user) => {
        console.log('User updated successfully', user);
      },
      (error) => {
        console.log('Error updating user', error);
      }
    );
  }
  onDelete():void{
    this.userService.deleteUser(this.user.id).subscribe(res=>(console.log(res)));
    
  }
}
