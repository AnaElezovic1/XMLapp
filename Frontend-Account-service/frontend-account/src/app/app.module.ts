import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { RegistrationPageComponent } from './registration-page/registration-page.component';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component';
import { HttpClientModule } from '@angular/common/http';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { AccommodationListComponent } from './accommodation-list/accommodation-list.component';
import { NewAccommodationComponent } from './new-accommodation/new-accommodation.component';
import { BookingListComponent } from './booking-list/booking-list.component';
import { CreateBookingComponent } from './new-booking/new-booking.component';
import { EditBookingComponent } from './edit-booking/edit-booking.component';
import { ReservationListComponent } from './reservation-list/reservation-list.component';

const appRoutes: Routes = [
  { path: 'registration', component: RegistrationPageComponent },
  { path: 'login', component: LoginPageComponent },
  { path: 'user-profile', component: UserProfileComponent },
  { path: 'accommodation-list', component: AccommodationListComponent},
  {path:'new-accommodation',component: NewAccommodationComponent},
  {path:'booking-list/:id',component:BookingListComponent},
  {path:'new-booking/:id',component:CreateBookingComponent},
  {path:'edit-booking/:id',component:EditBookingComponent},
{path:'reservation-list',component:ReservationListComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    RegistrationPageComponent,
    LoginPageComponent,
    UserProfileComponent,
    AccommodationListComponent,
    NewAccommodationComponent,
    BookingListComponent,
    CreateBookingComponent,
    EditBookingComponent,
    ReservationListComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
