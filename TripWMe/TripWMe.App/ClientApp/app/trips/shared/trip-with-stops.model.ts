import { Trip } from './trip.model';
import { Stop } from '../stops/shared/stop.model';

export class TripWithStops extends Trip {
    stops: Stop[];
}