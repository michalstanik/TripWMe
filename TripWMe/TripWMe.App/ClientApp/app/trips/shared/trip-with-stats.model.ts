export class TripWithStats {
    id: number;
    name: string;
    tripCode: string;
    tripStats: {
        locationCount: number;
        countryCount: number;
        userCount: number;
    }
    countryCodes: Array<string>
}