import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { TripService } from '../shared/services/trip.service';
import { GeoService } from '../../geo/shared/services/geo.service';

import * as mapsData from 'devextreme/dist/js/vectormap-data/world.js';
import {  MapService } from '../shared/services/map.service';
import { TripCountryWithAssessment } from 'ClientApp/app/shared/models/country/trip-country-with-assessment';



@Component({
    providers: [MapService],
    selector: 'trips-summary',
    templateUrl: "trips-summary.component.html",
    styleUrls: ["trips-summary.component.css"]
})

export class TripsSummaryComponent {
    worldMap: any = mapsData.world;
    countries: TripCountryWithAssessment[];
    countryTrip: any;
    entry = [];

    constructor(private tripService: TripService, private geoService: GeoService,
        private route: ActivatedRoute, private mapService: MapService) {

    }

    ngOnInit() {
        this.geoService.GetCountriesForAllTripsWithAssessment()
            .subscribe(countryTrip => {
                this.countryTrip = countryTrip;
            });

    }

}




