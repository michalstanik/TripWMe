import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { TripService } from '../../shared/trip.service';

@Component( {
    templateUrl: './trip-details.component.html'
})

export class TripDetailsComponent {
    trip: any

    constructor(private tripService: TripService, private route: ActivatedRoute) {

    }

    ngOnInit() {
        this.trip = this.tripService.getTrip(
            +this.route.snapshot.params['id']);
    }
}