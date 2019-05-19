import { Component, Input } from '@angular/core'
import { Router } from '@angular/router'

import { TripWithStats } from '../shared/model/trip-with-stats.model'

@Component({
    selector: 'trip-thumbnail',
    templateUrl: 'trip-thumbnail.component.html',
    styleUrls: ["trip-thumbnail.component.css"]
})

export class TripThumbnailComponent {
    @Input() trip: TripWithStats 


    constructor(private router: Router) {

    }

    navigate() {
        this.router.navigate(['/dashboard/trips', this.trip.id])
    }

}