export interface TripWithStats {
    id: number;
    tripnameName: string;
    tripCode: string;
    tripStats: {
        locationCount: number;
        countryCount: number;
        userCount: number;
    }
}