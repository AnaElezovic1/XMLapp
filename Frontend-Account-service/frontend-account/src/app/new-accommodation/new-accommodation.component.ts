import { Component } from '@angular/core';
import { Accommodation } from '../model/accommodation';
import { AccommodationService } from '../service/accommodation.service';
import { AuthService } from '../service/auth.service';
import { Users } from '../model/user';

@Component({
  selector: 'app-new-accommodation',
  templateUrl: './new-accommodation.component.html',
  styleUrls: ['./new-accommodation.component.css']
})

export class NewAccommodationComponent {
  accommodation: Accommodation = {
    id:new Number(),
    name: '',
    description: '',
    location: '',
    images: '',
    beds: 0,
    hostId: 0,
    
  };
  private user:Users={
    id:0,
    username:"user",
    password:"pass",
    email:"email",
    role:"H",
    adress:"nesto"
  }
  minGuests:number=1;
  maxGuests:number=6;
  constructor(private accommodationService: AccommodationService,private authService:AuthService) { }
  ngOnInit() {
   // this.authService.currentlyLoggedInUser(this.user);
  }
  onSubmit() {
    this.accommodation.hostId=this.authService.loggedInUser.id;
    this.accommodationService.addUser(this.accommodation).subscribe();
    console.log(this.accommodation);
  }
  convertToString(event:any)
  {
    var file = event.target.files[0]
    var reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = (event) => {
     this.accommodation.images = (<FileReader>event.target).result as string;
     console.log(this.accommodation.images);
    }

  }
}