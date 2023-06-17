import { Component } from '@angular/core';
import { Accommodation } from '../model/accommodation';
import { AccommodationService } from '../service/accommodation.service';
import { AuthService } from '../service/auth.service';

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
    hostId: this.authService.loggedInUser.id,
    
  };
  constructor(private accommodationService: AccommodationService,private authService:AuthService) { }
  onSubmit() {
    this.accommodationService.addUser(this.accommodation).subscribe();
    console.log(this.accommodation);
  }
}