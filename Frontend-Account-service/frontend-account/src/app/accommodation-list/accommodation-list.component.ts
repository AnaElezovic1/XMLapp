import { Component, OnInit } from '@angular/core';
import { Accommodation } from '../model/accommodation';
import { AccommodationService } from '../service/accommodation.service';
import { FormsModule } from '@angular/forms';
import { Users } from '../model/user';
import { AuthService } from '../service/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-accommodation-list',
  templateUrl: './accommodation-list.component.html',
  styleUrls: ['./accommodation-list.component.css']
})
export class AccommodationListComponent implements OnInit {
  accommodations: Accommodation[]=[];
  descriptions: string[]=[];
  sortedColumn: string="";
  reverseSort: boolean = false;
  searchBeds: number=0;
  searchLocation: string="";
  wantedDescription: string="";
  private user:Users={
    id:0,
    username:"user",
    password:"pass",
    email:"email",
    role:"HOST",
    adress:"nesto"
  }
  isHost:boolean=false;
  hostId:number=0;
  constructor(private route: ActivatedRoute, private router: Router, private httpClient: HttpClient,private authService:AuthService,private accommodationService: AccommodationService) { }

  ngOnInit(): void {
   // this.authService.currentlyLoggedInUser(this.user);
    if(this.authService.loggedInUser.role=="HOST"){
        this.isHost=true;
    }
    if(this.authService.loggedInUser.role=="HOST")
    {
      this.accommodationService.getByHost(this.authService.loggedInUser.id).subscribe((data: Accommodation[]) => {
        this.accommodations = data;    })}
    else{
    this.accommodationService.getAll().subscribe((data: Accommodation[]) => {
      this.accommodations = data;
      data.forEach(e=>this.descriptions.push(e.description));
    });
    }
  }

  sort(columnName: string): void {
    if (columnName === this.sortedColumn) {
      this.reverseSort = !this.reverseSort;
    } else {
      this.sortedColumn = columnName;
      this.reverseSort = false;
    }

    this.accommodations.sort((a, b) => {
      const A:any = a[columnName];
      const B:any = b[columnName];

      if (typeof A === 'string') {
        return this.reverseSort ? B.localeCompare(A) : A.localeCompare(B);
      } else {
        return this.reverseSort ? B - A : A - B;
      }
    });
  }

  filter(): Accommodation[] {
    let filteredAccommodations = this.accommodations;

    if (this.searchBeds) {
      filteredAccommodations = filteredAccommodations.filter(accommodation => accommodation.beds >= this.searchBeds);
    }

    if (this.searchLocation) {
      filteredAccommodations = filteredAccommodations.filter(accommodation => accommodation.location.toLowerCase().includes(this.searchLocation.toLowerCase()));
    }
    filteredAccommodations=filteredAccommodations.filter(accommodation => accommodation.description.toLowerCase().includes(this.wantedDescription.toLowerCase()))
    return filteredAccommodations;
  }
  onSelected(string:string)
  {
    this.wantedDescription=string;
    this.filter();
  }
}