import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { Subject, Observable, of } from 'rxjs';
import { TripCountry } from '../model/trip-country';

import { catchError } from 'rxjs/operators';
import { TripCountries } from '../model/trip-countries.model';


@Injectable()
export class GeoService {

    constructor(private http: HttpClient) { }

    getCountriesForTrip(id: number): Observable<TripCountry> {
        return this.http.get<TripCountry>('/api/geo/GetCountriesForTrip/' + id)
            .pipe(catchError(this.handleError<TripCountry>('getCountriesForTrip')))
    }

    GetCountriesForAllTrips(): Observable<TripCountry[]> {
        return this.http.get<TripCountry[]>('/api/geo/GetCountriesForAllTrips/')
            .pipe(catchError(this.handleError<TripCountry[]>('GetCountriesForAllTrips')))
    }

    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            console.error(error);
            return of(result as T);
        }
    }

}