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

import { TripsListComponent } from './trips/trips-list/trips-list.components';
import { TripsSummaryComponent } from './trips/trips-summary/trips-summary.component';
import { TripThumbnailComponent } from './trips/trips-list/trip-thumbnail.component';
import { StopsComponent } from './stops/stops.component';
import { TripDetailsComponent } from './trips/trip-details/trip-details.component';


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
        StopsComponent
    ],
    exports: [],
    providers: [TripService,MapService]
})
export class DashboardModule { }

