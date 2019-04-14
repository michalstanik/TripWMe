import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { TripService } from '../../shared/trip.service';
var TripDetailsComponent = /** @class */ (function () {
    function TripDetailsComponent(tripService) {
        this.tripService = tripService;
    }
    TripDetailsComponent.prototype.ngOnInit = function () {
        this.oneTrip = this.tripService.getTrip(1);
    };
    TripDetailsComponent = tslib_1.__decorate([
        Component({
            templateUrl: './trip-details.component.html'
        }),
        tslib_1.__metadata("design:paramtypes", [TripService])
    ], TripDetailsComponent);
    return TripDetailsComponent;
}());
export { TripDetailsComponent };
//# sourceMappingURL=trip-details.component.js.map