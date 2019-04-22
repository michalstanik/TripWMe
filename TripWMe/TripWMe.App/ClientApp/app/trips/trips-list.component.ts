import { Component, OnInit } from '@angular/core';

import { TripService } from './shared/trip.service';

import { ToastrService } from 'ngx-toastr';
@Component({
    selector: 'trips-list',
    templateUrl: "trips-list.component.html",
})
export class TripsListComponent implements OnInit {
    trips: any[]
    constructor(private tripService: TripService, private toastr: ToastrService) {
        
    }

    ngOnInit() {
        this.tripService.getTrips().subscribe(trips => {this.trips = trips})
    }

    handleThumbnailClick(tripCode) {
        this.toastr.success(tripCode)
    }
}
