import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Booking } from '../model/booking';
import { BookingService } from '../service/booking.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-booking',
  templateUrl: './edit-booking.component.html',
  styleUrls:['./edit-booking.component.css']
})
export class EditBookingComponent {
  bookingForm: FormGroup= this.formBuilder.group({
    start: ['', Validators.required],
    end: ['', Validators.required],
    price: ['', Validators.required],
    perperson: [false],
    autoaccept: [false],
    accommodationId: ['', Validators.required]
  });
  submitted = false;
  booking:Booking={
    id:0,
    start:new Date(),
    end: new Date,
    price:100,
    autoaccept:false,
    perperson:false,
    accommodationId:0
  }
  constructor(private formBuilder: FormBuilder,private bookingService:BookingService,private route:ActivatedRoute) {}

  ngOnInit() {
     this.bookingService.getById(parseInt(this.route.snapshot.paramMap.get('id') as string)).subscribe(res=>{this.booking=res;
      console.log(this.booking)}
      );
     this.bookingForm = this.formBuilder.group({
      start: [(this.booking as unknown as Booking).start, Validators.required],
      end: [(this.booking as unknown as Booking).end, Validators.required],
      price: [(this.booking as unknown as Booking).price, Validators.required],
      perperson: [(this.booking as unknown as Booking).perperson],
      autoaccept: [(this.booking as unknown as Booking).autoaccept],
      accommodationId: [(this.booking as unknown as Booking).accommodationId, Validators.required]
    });
  }

  get f() { return this.bookingForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.bookingForm.invalid) {
      return;
    }

    const newBooking: Booking = {
      id: parseInt(this.route.snapshot.paramMap.get('id') as string), // the ID will be assigned by the server
      start: this.f.start.value,
      end: this.f.end.value,
      price: this.f.price.value,
      perperson: this.f.perperson.value,
      autoaccept: this.f.autoaccept.value,
      accommodationId: this.f.accommodationId.value
    };
    this.bookingService.update(newBooking).subscribe();
    console.log(newBooking); // you can send the new booking to your API or handle it as needed
  }
}