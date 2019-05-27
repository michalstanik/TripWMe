import { Component, OnInit, Input } from '@angular/core';

import { TripCountryWithAssessment } from '../shared/model/trip-country-with-assessment';

@Component({
    selector: 'app-countries-list',
    templateUrl: './countries-list.component.html',
    styleUrls: ['./countries-list.component.scss']
})
export class CountriesListComponent implements OnInit {

    constructor() { }

    @Input() countryTrip: TripCountryWithAssessment[];

    ngOnInit() {
    }

}
