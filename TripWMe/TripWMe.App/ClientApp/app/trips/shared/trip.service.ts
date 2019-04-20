import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { ITripWithStats } from './tripWithStats.model';
import { Subject, Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';



@Injectable()
export class TripService {

    constructor(private http: HttpClient) { }

    getTrips(): Observable<ITripWithStats[]> {
        return this.http.get<ITripWithStats[]>('/api/trips/GetAllTripsWithStats')
            .pipe(catchError(this.handleError<ITripWithStats[]>('getTrips',[])))

    }

    getTrip(id: number): ITripWithStats {
        return TRIPS.find(trip => trip.id === id)
    }

    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            console.error(error);
            return of(result as T);
        }
    }
}


const TRIPS: ITripWithStats[] = [
    {
        id: 1,
        name: "Trip 1",
        tripCode: "TR-11",
        tripStats:
        {
            locationCount: 1,
            countryCount: 2,
            userCount: 3
        }
    },
   {
       id: 2,
       name: "Trip 2",
       tripCode: "TR-22",
       tripStats:
       {
            locationCount: 1,
            countryCount: 2,
            userCount: 3
        }
    },
    {
        id: 1, 
        name: "Trip 3",
        tripCode: "TR-33",
        tripStats: {
            locationCount: 1,
            countryCount: 2,
            userCount: 3
        }
    }
]
