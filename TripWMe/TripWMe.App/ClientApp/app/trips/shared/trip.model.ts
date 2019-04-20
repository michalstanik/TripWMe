
export class Stop {
    stopName: string;
    stopDescription: string;
    order: number;
    arrival: Date;
    departure: Date;
    locationName: string;
    locationDescription: string;
    locationLatitude: number;
    locationLongitude: number;
    locationCountryName: string;
    locationCountryAlpha2Code: string;
    locationLocationTypeName: string;
}

export class User {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
}

export class Trip {
    name: string;
    tripCode: string;
    starRating: number;
    stops: Array<Stop> = new Array<Stop>();
    users: Array<User> = new Array<User>();
}