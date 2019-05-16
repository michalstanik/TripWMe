import { TripWithStops } from './trip-with-stops.model';
import { User } from './user.model';


export class TripWithStopsAndUsers extends TripWithStops {
    users: User[];
}