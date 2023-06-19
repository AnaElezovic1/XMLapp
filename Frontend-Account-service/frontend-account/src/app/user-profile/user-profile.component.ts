import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { Users } from '../model/user';
import { UserService } from '../service/user.service';
import { RateHost } from '../model/rateHost';
import { RateAcc } from '../model/rateAcc';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css'],
})
export class UserProfileComponent implements OnInit {
  user!: Users;
  users!: Users[];
  loggedIn = false;
  rateHost!: RateHost;
  rateAcc!:RateAcc;
  showUpdateCentre: boolean = false;
  showUpdateUser1: boolean = false;
  showUpdateCentre1: boolean = false;
  showUpdateCentre2: boolean = false;

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

  onRateHost(){

    this.userService.rateHost(this.rateHost).subscribe(
      (user) => {
        console.log('User created successfully', user);
      },
      (error) => {
        console.log('Error updating user', error);
      }
    );

  }

  onRateAcc(){

    this.userService.rateAcc(this.rateAcc).subscribe(
      (user) => {
        console.log('User created successfully', user);
      },
      (error) => {
        console.log('Error updating user', error);
      }
    );

  }

  //update
  onUpdateRate(){

    this.userService.updateHost(this.rateHost).subscribe(
      (user) => {
        console.log('User created successfully', user);
      },
      (error) => {
        console.log('Error updating user', error);
      }
    );

  }

  onUpdateRateAcc(){
    this.userService.updateAcc(this.rateAcc).subscribe(
      (user) => {
        console.log('User created successfully', user);
      },
      (error) => {
        console.log('Error updating user', error);
      }
    );
  }

  showRateForm(){
    this.showUpdateCentre = true;
  }

  showRateAcc(){
    this.showUpdateUser1 = true;
  }

  //update
  showRateForm1(){
    this.showUpdateCentre1 = true;
  }

  showRateAcc1(){
    this.showUpdateCentre2 = true;
  }
}
