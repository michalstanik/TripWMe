import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TripDashboardComponent } from './trips/trips-dashboard.component';
import { TripsListComponent } from './trips/trips-list.component';
import { TripThumbnailComponent } from './trips/trip-thumbnail.component';
import { TripDetailsComponent } from './trips/trip-details/trip-details.component'

import { TripService } from './shared/trip.service';

import { appRoutes } from "./routes"
import { RouterModule } from '@angular/router';

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
        RouterModule.forRoot(appRoutes)
  ],
  providers: [TripService],
  bootstrap: [AppComponent]
})
export class AppModule { }
