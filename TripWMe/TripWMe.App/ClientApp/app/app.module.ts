import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TripsListComponent } from './trips/trips-list.component';
import { TripThumbnailComponent } from './trips/trip-thumbnail.component';

import { TripService } from './shared/trip.service';

@NgModule({
  declarations: [
      AppComponent,
      TripsListComponent,
      TripThumbnailComponent
  ],
    imports: [
        BrowserModule,
        NgbModule
  ],
  providers: [TripService],
  bootstrap: [AppComponent]
})
export class AppModule { }
