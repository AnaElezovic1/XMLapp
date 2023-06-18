import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Booking } from '../model/booking';
import { BookingService } from '../service/booking.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-new-booking',
  templateUrl: './new-booking.component.html',
})
export class CreateBookingComponent {
  bookingForm: FormGroup= this.formBuilder.group({
    start: ['', Validators.required],
    end: ['', Validators.required],
    price: ['', Validators.required],
    perperson: [false],
    autoaccept: [false],
    accommodationId: ['', Validators.required]
  });
  submitted = false;

  constructor(private route: ActivatedRoute, private router: Router, private httpClient: HttpClient,private formBuilder: FormBuilder,private bookingService:BookingService) {}

  ngOnInit() {
    this.bookingForm = this.formBuilder.group({
      start: ['', Validators.required],
      end: ['', Validators.required],
      price: ['', Validators.required],
      perperson: [false],
      autoaccept: [false],
      accommodationId:(parseInt(this.route.snapshot.paramMap.get('id') as string))
    });
  }

  get f() { return this.bookingForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.bookingForm.invalid) {
      return;
    }

    const newBooking: Booking = {
      id: new Number(), // the ID will be assigned by the server
      start: this.f.start.value,
      end: this.f.end.value,
      price: this.f.price.value,
      perperson: this.f.perperson.value,
      autoaccept: this.f.autoaccept.value,
      accommodationId: this.f.accommodationId.value,
    };
    this.bookingService.addUser(newBooking).subscribe();
    console.log(newBooking); // you can send the new booking to your API or handle it as needed
  }
}