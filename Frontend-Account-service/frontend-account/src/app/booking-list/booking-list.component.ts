import { Component } from '@angular/core';
import { Booking } from '../model/booking';
import { BookingService } from '../service/booking.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AccommodationService } from '../service/accommodation.service';
import { Accommodation } from '../model/accommodation';
import { AuthService } from '../service/auth.service';
import { Users } from '../model/user';
import { ReservationService } from '../service/reservation.service';
import { Reservation } from '../model/reservation';
import { UserService } from '../service/user.service';


@Component({
  selector: 'app-booking-list',
  templateUrl: './booking-list.component.html',
  styleUrls: ['./booking-list.component.css']

})
export class BookingListComponent {
  minPrice: number=1;
maxPrice: number=30000;
  bookings: Booking[]=[];
  sortedBookings: Booking[]=[];
  isAscending = true;
  searchText: string="";
  isHost:boolean=false;
  isGuest:boolean=true;
  private user:Users={
    id:0,
    username:"user",
    password:"pass",
    email:"email",
    role:"H",
    adress:"nesto"
  }
  noOfGuests:number=1;
  constructor(private userService:UserService,private reservationService:ReservationService,private authService:AuthService,private accommodationService:AccommodationService,private bookingService: BookingService,private route: ActivatedRoute, private router: Router, private httpClient: HttpClient) {
    this.getBookings();
  }
  ngOnInit(): void {

    this.userService.getUsers().subscribe((users) => {
     // this.users = users;
    //  console.log(this.users);
    //  this.loggedIn = this.authService.isLoggedIn;
      this.user = this.authService.loggedInUser;
    });
   console.log(this.authService.loggedInUser);
   this.user=this.authService.loggedInUser;
   console.log(this.user);
    if(this.authService.loggedInUser.role=="HOST"){
        this.isHost=true;
        this.isGuest=false;
    }
  }
  getBookings(): void {
    this.bookingService.getByAccommodation(parseInt(this.route.snapshot.paramMap.get('id') as string)).subscribe(bookings => {
      this.bookings = bookings;
      this.sortedBookings = bookings.slice(); // make a copy for sorting
    });
  }

  sortBookings(prop: keyof Booking): void {
    this.isAscending = !this.isAscending;
    const direction = this.isAscending ? 1 : -1;
    this.sortedBookings.sort((a, b) => {
      if (a[prop] < b[prop]) {
        return -1 * direction;
      } else if (a[prop] > b[prop]) {
        return 1 * direction;
      } else {
        return 0;
      }
    });
  }

  searchBookings(): void {
    if (!this.searchText && !this.minPrice && !this.minPrice) {
      this.sortedBookings = this.bookings.slice(); // restore original order
      return;
    }
    this.sortedBookings = this.bookings.filter(booking => {
      const startDateStr = booking.start.toString();
      const endDateStr = booking.end.toString();
      const isInPriceRange = (booking.price >= this.minPrice )
                            && ( booking.price <= this.maxPrice );
      console.log(startDateStr);
      return startDateStr.includes(this.searchText)
          && endDateStr.includes(this.searchText)
          && isInPriceRange;
    });
  }
  totalPrice(booking:Booking)
  {
    
    var diff=new Date(booking.end).getTime()-new Date(booking.start).getTime();
    var diffDays = Math.ceil(diff / (1000 * 3600 * 24)); 
    if(booking.perperson==false){
    return booking.price*(diffDays)
    }
    else
  {
    
    return this.noOfGuests*booking.price*(diffDays);
  }
  }
  post(bid:Number)
  {
    var reservation:Reservation={
      id:0,
      guestId:this.authService.loggedInUser.id,
      bookingId:bid as number,
      noOfGuests:this.noOfGuests,
      accepted:false
    };
   this.reservationService.addUser(reservation).subscribe() ; 
   console.log(reservation);
  }
}