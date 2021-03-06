﻿import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { Subject, Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import {
    Trip,
    TripWithStats,
    TripWithTripManager,
    TripWithStops,
    TripWithStopsAndUsers,
} from '../model/index';


@Injectable()
export class TripService {

    constructor(private http: HttpClient) { }

    getTrips(): Observable<TripWithStats[]> {
        return this.http.get<TripWithStats[]>('/api/trips/GetAllTripsWithStats')
            .pipe(catchError(this.handleError<TripWithStats[]>('getTrips', [])))
    }

    getTrip(id: number): Observable<Trip> {
        return this.http.get<Trip>('/api/trips/' + id,
            { headers: { 'Accept': 'application/vnd.tripwme.trip+json' } })
            .pipe(catchError(this.handleError<Trip>('getTrips')))
    }

    getTripWithTripManger(id: number): Observable<TripWithTripManager> {
        return this.http.get<TripWithTripManager>('/api/trips/' + id,
            { headers: { 'Accept': 'application/vnd.tripwme.tripwithtripmanager+json' } })
            .pipe(catchError(this.handleError<TripWithTripManager>('getTripsWithManagers')))
    }

    getTripWitStops(id: number): Observable<TripWithStops> {
        return this.http.get<TripWithStops>('/api/trips/' + id,
            { headers: { 'Accept': 'application/vnd.tripwme.tripwithstops+json' } })
            .pipe(catchError(this.handleError<TripWithStops>('getTripWithStops')))
    }

    getTripWitStopsAndUsers(id: number): Observable<TripWithStopsAndUsers> {
        return this.http.get<TripWithStopsAndUsers>('/api/trips/' + id,
            { headers: { 'Accept': 'application/vnd.tripwme.tripwithstops+json' } })
            .pipe(catchError(this.handleError<TripWithStopsAndUsers>('getTripWitStopsAndUsers')))
    }

    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            console.error(error);
            return of(result as T);
        }
    }
}

