import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { TripService } from '../shared/trip.service';
var TripsListComponent = /** @class */ (function () {
    function TripsListComponent(tripService) {
        this.tripService = tripService;
    }
    TripsListComponent.prototype.ngOnInit = function () {
        this.trips = this.tripService.getTrips();
    };
    TripsListComponent = tslib_1.__decorate([
        Component({
            selector: 'trips-list',
            templateUrl: "trips-list.component.html",
        }),
        tslib_1.__metadata("design:paramtypes", [TripService])
    ], TripsListComponent);
    return TripsListComponent;
}());
export { TripsListComponent };
//# sourceMappingURL=trips-list.component.js.map