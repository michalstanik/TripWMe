export class TripWithStats {
    id: number;
    name: string;
    tripCode: string;
    tripStats: {
        stopCount: number;
        locationCount: number;
        countryCount: number;
        userCount: number;
    }
    countryCodes: Array<string>;
    startDate: Date;
    endDate: Date;
}