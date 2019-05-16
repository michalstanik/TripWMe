import { Component, OnInit, Input } from '@angular/core';
import { Stop } from './shared/stop.model';

@Component({
    selector: 'app-stops',
    templateUrl: './stops.component.html',
    styleUrls: ['./stops.component.css']
})

export class StopsComponent implements OnInit {

    @Input() stops: Stop[];

    ngOnInit() {
    }
}
