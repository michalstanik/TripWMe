import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { DxVectorMapModule } from 'devextreme-angular';

import {
    TripDashboardComponent,
    TripsListComponent,
    TripThumbnailComponent,
    TripDetailsComponent,
    TripService,
    MapService
} from './trips/index';

import { AppComponent } from './app.component';

import { appRoutes } from "./routes";





@NgModule({
  declarations: [
      AppComponent,
      TripDashboardComponent,
      TripsListComponent,
      TripThumbnailComponent,
      TripDetailsComponent
  ],
    imports: [
        BrowserModule,
        NgbModule,
        FormsModule,
        RouterModule.forRoot(appRoutes, {
            useHash:true
        }),
        HttpClientModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        DxVectorMapModule
  ],
    providers: [TripService, MapService],
  bootstrap: [AppComponent]
})
export class AppModule { }
