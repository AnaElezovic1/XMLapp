import { Component } from '@angular/core';
import { Booking } from '../model/booking';
import { BookingService } from '../service/booking.service';


@Component({
  selector: 'app-booking-list',
  templateUrl: './booking-list.component.html',
})
export class BookingListComponent {

  bookings: Booking[]=[];
  sortedBookings: Booking[]=[];
  isAscending = true;
  searchText: string="";

  constructor(private bookingService: BookingService) {
    this.getBookings();
  }

  getBookings(): void {
    this.bookingService.getAll().subscribe(bookings => {
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
    if (!this.searchText) {
      this.sortedBookings = this.bookings.slice(); // restore original order
      return;
    }
    this.sortedBookings = this.bookings.filter(booking => {
      const startDateStr = booking.start.toString();
      const endDateStr = booking.end.toString();
      return startDateStr.includes(this.searchText)
          || endDateStr.includes(this.searchText);
    });
  }
}