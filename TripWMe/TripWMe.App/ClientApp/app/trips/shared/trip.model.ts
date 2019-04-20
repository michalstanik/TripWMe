export interface ITrip {
    name: string
    tripCode: string
    starRating: number
    stops: {
          stopName: string
          stopDescription: string
          order: number
          arrival: Date
          departure: Date
          locationName: string
          locationDescription: string
          locationLatitude: number
          locationLongitude: number
          locationCountryName: string
          locationCountryAlpha2Code: string
          locationLocationTypeName: string
        }
    users:{
            id: string
            firstName: string
            lastName: string,
            email: string
        }
}