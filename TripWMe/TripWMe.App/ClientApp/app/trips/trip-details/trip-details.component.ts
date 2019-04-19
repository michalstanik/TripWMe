import { Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { TripService } from '../shared/trip.service';

@Component( {
    templateUrl: './trip-details.component.html'
})

export class TripDetailsComponent {
    trip: any

    constructor(private tripService: TripService, private route: ActivatedRoute) {

    }

    ngOnInit() {
        this.route.params.forEach((params: Params) => {
            this.trip = this.tripService.getTrip(+params['id'])
        })
    }

}