import { Component, OnInit, Input } from '@angular/core';
import { TripCountry } from '../shared/model/trip-country';

@Component({
    selector: 'app-countries-list',
    templateUrl: './countries-list.component.html',
    styleUrls: ['./countries-list.component.scss']
})
export class CountriesListComponent implements OnInit {

    constructor() { }

    @Input() countryTrip: TripCountry[];

    ngOnInit() {
    }

}
