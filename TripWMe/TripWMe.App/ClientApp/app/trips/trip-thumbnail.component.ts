import { Component, Input } from '@angular/core'

@Component({
    selector: 'trip-thumbnail',
    templateUrl: 'trip-thumbnail.component.html' 
})

export class TripThumbnailComponent {
   @Input() trip:any
}