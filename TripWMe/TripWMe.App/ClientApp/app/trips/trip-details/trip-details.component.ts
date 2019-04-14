import { Component } from '@angular/core';

import { TripService } from '../../shared/trip.service';

@Component( {
    templateUrl: './trip-details.component.html'
})

export class TripDetailsComponent {
    oneTrip: any

    constructor(private tripService: TripService) {

    }

    ngOnInit() {
        this.oneTrip = this.tripService.getTrip(1);
    }
}