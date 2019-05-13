import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { DxVectorMapModule } from 'devextreme-angular';
import { MDBBootstrapModulesPro } from 'ng-uikit-pro-standard';
import { MDBSpinningPreloader } from 'ng-uikit-pro-standard';

import {
    TripDashboardComponent,
    TripsListComponent,
    TripThumbnailComponent,
    TripDetailsComponent,
    TripsSummaryComponent,
    TripService,
    MapService
} from './trips/index';

import { AppComponent } from './app.component';

import { appRoutes } from "./routes";
import { EnsureAcceptHeaderInterceptor } from './shared/ensure-accept-header-interceptor';


@NgModule({
  declarations: [
      AppComponent,
      TripDashboardComponent,
      TripsListComponent,
      TripThumbnailComponent,
      TripDetailsComponent,
      TripsSummaryComponent
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
        DxVectorMapModule,
        MDBBootstrapModulesPro.forRoot()

  ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: EnsureAcceptHeaderInterceptor,
            multi: true
        },
        MDBSpinningPreloader, TripService, MapService],
  bootstrap: [AppComponent]
})
export class AppModule { }
