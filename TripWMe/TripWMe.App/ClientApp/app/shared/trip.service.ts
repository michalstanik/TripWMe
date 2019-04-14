import { Injectable } from '@angular/core'

@Injectable()
export class TripService {
    getTrips() {
        return TRIPS
    }
}

    const TRIPS = [
        { name: 'Trip 1 NG' },
        { name: 'Trip 2 NG' },
        { name: 'Trip 3 NG' },
        { name: 'Trip 4 NG' },
        { name: 'Trip 5 NG' },
        { name: 'Trip 6 NG' },
        { name: 'Trip 7 NG' },
    ]
