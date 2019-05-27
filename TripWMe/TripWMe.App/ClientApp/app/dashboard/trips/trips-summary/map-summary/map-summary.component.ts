import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import * as mapsData from 'devextreme/dist/js/vectormap-data/world.js';

import { TripCountry } from 'ClientApp/app/dashboard/geo/shared/model/trip-country';

import { GeoService } from 'ClientApp/app/dashboard/geo/shared/services/geo.service';


@Component({
  selector: 'app-map-summary',
  templateUrl: './map-summary.component.html',
  styleUrls: ['./map-summary.component.scss']
})
export class MapSummaryComponent implements OnInit {
    worldMap: any = mapsData.world;
    countries: TripCountry[];

    constructor(private geoService: GeoService, private route: ActivatedRoute) { }

    ngOnInit() {
        this.geoService.GetCountriesForAllTrips()
            .subscribe(countryTrips => {
                this.countries = countryTrips as TripCountry[];
            });

        this.customizeLayers = this.customizeLayers.bind(this);
    }

    customizeLayers(elements) {
        elements.forEach((element) => {

            for (let entry of this.countries) {
                //console.log('Name: ', entry.name);
                if (entry.name === element.attribute("name")) {
                    console.log('Map element ', element.attribute("name"));
                    element.applySettings({
                        color: "#008f00",
                        hoveredColor: "#e0e000",
                        selectedColor: "#008f00"
                    });
                }
            }
        });
    }
}
