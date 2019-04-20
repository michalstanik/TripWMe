export interface ITripWithStats {
    id: number;
    name: string;
    tripCode: string;
    tripStats: {
        locationCount: number;
        countryCount: number;
        userCount: number;
    }
}