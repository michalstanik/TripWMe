import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import {
    TripDashboardComponent,
    TripsListComponent,
    TripThumbnailComponent,
    TripDetailsComponent
} from './trips/index';

//import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { appRoutes } from "./routes"
import { TripService } from './trips/shared/trip.service';



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
        ToastrModule.forRoot()
  ],
  providers: [TripService],
  bootstrap: [AppComponent]
})
export class AppModule { }
