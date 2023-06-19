import { Component } from '@angular/core';
import { Users } from '../model/user';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent {
  private user:Users={
    id:0,
    username:"user",
    password:"pass",
    email:"email",
    role:"H",
    adress:"nesto"
  }
  isHost:boolean=false;
  isGuest:boolean=true;
constructor(private authService:AuthService){}
ngOnInit(){
//this.authService.currentlyLoggedInUser(this.user);
if(this.authService.loggedInUser.role=="H"){
    this.isHost=true;
    this.isGuest=false;
}
}
}
