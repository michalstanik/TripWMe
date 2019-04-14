import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
var TripService = /** @class */ (function () {
    function TripService() {
    }
    TripService.prototype.getTrips = function () {
        return TRIPS;
    };
    TripService.prototype.getTrip = function (id) {
        return TRIPS.find(function (trip) { return trip.id === 1; });
    };
    TripService = tslib_1.__decorate([
        Injectable()
    ], TripService);
    return TripService;
}());
export { TripService };
var TRIPS = [
    { id: 1, name: 'Trip 1 NG' },
    { id: 2, name: 'Trip 2 NG' },
    { id: 3, name: 'Trip 3 NG' },
    { id: 4, name: 'Trip 4 NG' },
    { id: 5, name: 'Trip 5 NG' },
    { id: 6, name: 'Trip 6 NG' },
    { id: 7, name: 'Trip 7 NG' },
];
//# sourceMappingURL=trip.service.js.map