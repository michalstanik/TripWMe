import { Component, OnInit } from '@angular/core';
import { TripCountryWithAssessment } from 'ClientApp/app/shared/models/country/trip-country-with-assessment';
import { ActivatedRoute } from '@angular/router';

import * as mapsData from 'devextreme/dist/js/vectormap-data/world.js';
import { MyCountryService } from '../shared/services/mycountry.service';

@Component({
  selector: 'app-my-country-map',
  templateUrl: './my-country-map.component.html',
  styleUrls: ['./my-country-map.component.scss']
})
export class MyCountryMapComponent implements OnInit {

    worldMap: any = mapsData.world;
    countries: TripCountryWithAssessment[];

    constructor(private myCountryService: MyCountryService, private route: ActivatedRoute) { }

    ngOnInit() {
        this.myCountryService.GetCountriesForAllTripsWithAssessment()
            .subscribe(countryTrips => {
                this.countries = countryTrips as TripCountryWithAssessment[];
            });

        this.customizeLayers = this.customizeLayers.bind(this);
    }

    customizeLayers(elements) {
        elements.forEach((element) => {

            for (let entry of this.countries) {
                if (entry.name === element.attribute("name")) {
                    console.log('Map element ', element.attribute("name"));

                    switch (entry.areaLevelAssessment) {
                        case 100:
                            element.applySettings({
                                color: "#0B2146",
                                hoveredColor: "#e0e000",
                                selectedColor: "#008f00"
                            });
                            break;
                        case 90:
                            element.applySettings({
                                color: "#0F3956",
                                hoveredColor: "#e0e000",
                                selectedColor: "#008f00"
                            });
                            break;
                        case 80:
                            element.applySettings({
                                color: "#135666",
                                hoveredColor: "#e0e000",
                                selectedColor: "#008f00"
                            });
                            break;
                        case 70:
                            element.applySettings({
                                color: "#187573",
                                hoveredColor: "#e0e000",
                                selectedColor: "#008f00"
                            });
                            break;
                        case 60:
                            element.applySettings({
                                color: "#1D846C",
                                hoveredColor: "#e0e000",
                                selectedColor: "#008f00"
                            });
                            break;
                        case 50:
                            element.applySettings({
                                color: "#3A966F",
                                hoveredColor: "#e0e000",
                                selectedColor: "#008f00"
                            });
                            break;
                        case 40:
                            element.applySettings({
                                color: "#57A777",
                                hoveredColor: "#e0e000",
                                selectedColor: "#008f00"
                            });
                            break;
                        case 30:
                            element.applySettings({
                                color: "#74B883",
                                hoveredColor: "#e0e000",
                                selectedColor: "#008f00"
                            });
                            break;
                        case 20:
                            element.applySettings({
                                color: "#92C994",
                                hoveredColor: "#e0e000",
                                selectedColor: "#008f00"
                            });
                            break;
                        case 10:
                            element.applySettings({
                                color: "#B7D9B1",
                                hoveredColor: "#e0e000",
                                selectedColor: "#008f00"
                            });
                            break;
                        case 0:
                            element.applySettings({
                                color: "#D8E9D0",
                                hoveredColor: "#e0e000",
                                selectedColor: "#008f00"
                            });
                            break;
                    }
                }
            }
        });
    }
}
