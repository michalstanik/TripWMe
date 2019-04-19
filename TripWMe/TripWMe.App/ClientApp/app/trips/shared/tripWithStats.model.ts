export interface ITripWithStats {
    id: number;
    tripName: string;
    tripCode: string;
    tripStats: {
        locationCount: number;
        countryCount: number;
        userCount: number;
    }
}