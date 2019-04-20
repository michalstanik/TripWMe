import { Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { TripService } from '../shared/trip.service';
import { Trip } from '../shared/trip.model';

@Component( {
    templateUrl: './trip-details.component.html'
})

export class TripDetailsComponent {
    trip: Trip

    constructor(private tripService: TripService, private route: ActivatedRoute) {

    }

    ngOnInit() {
        this.route.params.forEach((params: Params) => {
            this.tripService.getTrip(+params["id"]).subscribe((trip: Trip) => {
                this.trip = trip;
            })
        })
    }

}