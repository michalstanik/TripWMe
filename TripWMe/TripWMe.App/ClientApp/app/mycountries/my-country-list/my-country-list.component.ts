import { Component, OnInit } from '@angular/core';

//Services
import { MyCountryService } from '../shared/services/mycountry.service';
//Models
import { TripCountryWithAssessment } from 'ClientApp/app/shared/models/country/trip-country-with-assessment';

@Component({
    selector: 'app-my-country-list',
    templateUrl: './my-country-list.component.html',
    styleUrls: ['./my-country-list.component.scss']
})
export class MyCountryListComponent implements OnInit {

    myCountries: TripCountryWithAssessment[];

    constructor(private myCountryService: MyCountryService) { }

    ngOnInit() {
        this.myCountryService.GetCountriesForAllTripsWithAssessment().subscribe(myCountries => { this.myCountries = myCountries });
    }

}
