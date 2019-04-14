import { Injectable } from '@angular/core'

@Injectable()
export class TripService {
    getTrips() {
        return TRIPS
    }

    getTrip(id: number) {
        return TRIPS.find(trip => trip.id === 1)
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
