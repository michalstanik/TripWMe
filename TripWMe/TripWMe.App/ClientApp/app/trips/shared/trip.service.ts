import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ITripWithStats } from './tripWithStats.model';



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

    getTrip(id: number): ITripWithStats {
        return TRIPS.find(trip => trip.id === id)
    }
}


const TRIPS: ITripWithStats[] = [
    {
        id: 1,
        tripName: "Trip 1",
        tripCode: "TR-1",
        tripStats:
        {
            locationCount: 1,
            countryCount: 2,
            userCount: 3
        }
    },
   {
       id: 2,
       tripName: "Trip 2",
       tripCode: "TR-2",
       tripStats:
       {
            locationCount: 1,
            countryCount: 2,
            userCount: 3
        }
    },
    {
        id: 1, 
        tripName: "Trip 3",
        tripCode: "TR-3",
        tripStats: {
            locationCount: 1,
            countryCount: 2,
            userCount: 3
        }
    }
]
