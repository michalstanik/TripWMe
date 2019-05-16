import { Trip } from './trip.model';
import { User } from './user.model';

export class TripWithTripManager extends Trip {
    tripManager: User
}