import { Component, Input } from '@angular/core'
import { Router } from '@angular/router'

import { ITripWithStats } from './shared/tripWithStats.model';

@Component({
    selector: 'trip-thumbnail',
    templateUrl: 'trip-thumbnail.component.html',
    styleUrls: ["trip-thumbnail.component.css"]
})

export class TripThumbnailComponent {
    @Input() trip: ITripWithStats 


    constructor(private router: Router) {

    }

    navigate() {
        this.router.navigate(['/trips', this.trip.id])
    }

}