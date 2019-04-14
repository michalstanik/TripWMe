import { Routes } from '@angular/router'
import { TripDetailsComponent } from './trips/trip-details/trip-details.component'
import { TripsListComponent } from './trips/trips-list.component'

export const appRoutes:Routes = [
    { path: 'trips/:id', component: TripDetailsComponent }
    

]