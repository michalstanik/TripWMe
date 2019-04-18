import { Component, Input } from '@angular/core'
import { Router } from '@angular/router'

@Component({
    selector: 'trip-thumbnail',
    templateUrl: 'trip-thumbnail.component.html',
    styleUrls: ["trip-thumbnail.component.css"]
})

export class TripThumbnailComponent {
    @Input() trip: any

    constructor(private router: Router) {

    }

    navigate() {
        this.router.navigate(['/trips', this.trip.id])
    }

}