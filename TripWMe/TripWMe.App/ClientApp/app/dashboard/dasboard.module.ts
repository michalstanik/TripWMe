import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { routing } from './dashboard.routing';
import { RootComponent } from './root/root.component';

import { MDBBootstrapModulesPro } from 'ng-uikit-pro-standard';
import { DxVectorMapModule } from 'devextreme-angular';
import { ToastrModule } from 'ngx-toastr';

import { TripService } from './trips/shared/services/trip.service';
import { MapService } from './trips/shared/services/map.service';
import { GeoService } from './geo/shared/services/geo.service';

import { TripsListComponent } from './trips/trips-list/trips-list.components';
import { TripsSummaryComponent } from './trips/trips-summary/trips-summary.component';
import { TripThumbnailComponent } from './trips/trips-list/trip-thumbnail.component';
import { StopsComponent } from './stops/stops.component';
import { TripDetailsComponent } from './trips/trip-details/trip-details.component';

import { CountriesListComponent } from './geo/countries-list/countries-list.component';;
import { MapSummaryComponent } from './trips/trips-summary/map-summary/map-summary.component'


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        routing,
        MDBBootstrapModulesPro.forRoot(),
        ToastrModule.forRoot(),
        DxVectorMapModule
    ],
    declarations: [
        RootComponent,
        TripsListComponent,
        TripThumbnailComponent,
        TripsSummaryComponent,
        TripDetailsComponent,
        StopsComponent,
        CountriesListComponent,        MapSummaryComponent
    ],
    exports: [],
    providers: [TripService, MapService, GeoService]
})
export class DashboardModule { }

