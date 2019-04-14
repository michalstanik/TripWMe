import * as tslib_1 from "tslib";
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from './app.component';
import { TripDashboardComponent } from './trips/trips-dashboard.component';
import { TripsListComponent } from './trips/trips-list.component';
import { TripThumbnailComponent } from './trips/trip-thumbnail.component';
import { TripDetailsComponent } from './trips/trip-details/trip-details.component';
import { TripService } from './shared/trip.service';
import { appRoutes } from "./routes";
import { RouterModule } from '@angular/router';
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = tslib_1.__decorate([
        NgModule({
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
    ], AppModule);
    return AppModule;
}());
export { AppModule };
//# sourceMappingURL=app.module.js.map