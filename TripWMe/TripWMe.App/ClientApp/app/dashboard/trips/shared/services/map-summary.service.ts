import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { TripCountryWithAssessment } from 'ClientApp/app/shared/models/country/trip-country-with-assessment';




@Injectable()
export class MapSummaryService {

    constructor(private http: HttpClient) { }

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
