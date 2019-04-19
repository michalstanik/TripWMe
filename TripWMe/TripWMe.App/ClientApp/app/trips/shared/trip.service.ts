import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";



@Injectable()
export class TripService {

    constructor(private http: HttpClient) { }

    //public trips: TripWithStats[] = [];

    getTrips() {
        return TRIPS

    }

    //loadTrips(): Observable<boolean> {
    //    return this.http.get("/api/trips/GetAllTripsWithStats")
    //        .map((data: any[]) => {
    //            this.trips = data;
    //            return true;
    //        });
    //}

    getTrip(id: number) {
        return TRIPS.find(trip => trip.id === id)
    }
}


    const TRIPS = [
        { id: 1, name: 'Trip 1 NG' },
        { id: 2, name: 'Trip 2 NG' },
        { id: 3, name: 'Trip 3 NG' },
        { id: 4, name: 'Trip 4 NG' },
        { id: 5, name: 'Trip 5 NG' },
        { id: 6, name: 'Trip 6 NG' },
        { id: 7, name: 'Trip 7 NG' },
    ]
