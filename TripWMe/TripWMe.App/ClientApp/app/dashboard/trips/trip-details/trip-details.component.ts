﻿import { Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { TripService } from '../shared/services/trip.service';
import { GeoService } from '../../geo/shared/services/geo.service';
import { Trip } from '../shared/model/trip.model';

import * as mapsData from 'devextreme/dist/js/vectormap-data/world.js';
import { Countries, MapService } from '../shared/services/map.service';

@Component({
    providers: [MapService],
    templateUrl: './trip-details.component.html'
})

export class TripDetailsComponent {
    trip: any;
    countryTrip: any;
    worldMap: any = mapsData.world;
    countries: Countries;

    constructor(private tripService: TripService, private geoService: GeoService,
        private route: ActivatedRoute, private mapService: MapService) {

        this.countries = mapService.getCountries();
        this.customizeTooltip = this.customizeTooltip.bind(this);
        this.customizeLayers = this.customizeLayers.bind(this);
        this.click = this.click.bind(this);
    }


    ngOnInit() {
        this.route.params.forEach((params: Params) => {
            this.tripService.getTripWitStopsAndUsers(+params["id"])
                .subscribe(trip => {
                    this.trip = trip;
                });
            this.geoService.getCountriesForTrip(+params["id"])
                .subscribe(countryTrip => {
                    this.countryTrip = countryTrip;
                });
        })
    }

    customizeTooltip(arg) {
        let name = arg.attribute("name"),
            country = this.countries[name];
        if (country) {
            return {
                text: name + ": " + country.totalArea + "M km&#178",
                color: country.color
            };
        }
    }

    customizeLayers(elements) {
        elements.forEach((element) => {
            let country = this.countries[element.attribute("name")];
            if (country) {
                element.applySettings({
                    color: country.color,
                    hoveredColor: "#e0e000",
                    selectedColor: "#008f00"
                });
            };
        });
    }

    click(e) {
        let target = e.target;
        if (target && this.countries[target.attribute("name")]) {
            target.selected(!target.selected());
        }
    }

}