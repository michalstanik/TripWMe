import { Component, OnInit } from '@angular/core';

import { TripService } from './shared/trip.service';

@Component({
    selector: 'trips-list',
    templateUrl: "trips-list.component.html",
})
export class TripsListComponent implements OnInit {
    trips: any[]
    constructor(private tripService: TripService) {
        
    }

    ngOnInit() {
        this.tripService.getTrips().subscribe(trips => {this.trips = trips})
    }
}