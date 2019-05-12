import { Trip, User } from './trip.model';

export class TripWithTripManager extends Trip {
    tripManager: User
}