﻿import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { Subject, Observable, of } from 'rxjs';

import { catchError } from 'rxjs/operators';

import { TripCountry } from 'ClientApp/app/shared/models/country/trip-country';
import { TripCountryWithAssessment } from 'ClientApp/app/shared/models/country/trip-country-with-assessment';

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

    GetCountriesForAllTripsWithAssessment(): Observable<TripCountryWithAssessment[]> {
        return this.http.get<TripCountryWithAssessment[]>('/api/geo/GetCountriesForAllTripsWithAssessments/')
            .pipe(catchError(this.handleError<TripCountryWithAssessment[]>('GetCountriesForAllTripsWithAssessment')))
    }

    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            console.error(error);
            return of(result as T);
        }
    }

}