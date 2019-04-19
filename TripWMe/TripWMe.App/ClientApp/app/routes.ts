import { Routes } from '@angular/router'

import {
    TripDetailsComponent,
    TripsListComponent
} from './trips/index';

export const appRoutes:Routes = [
    { path: 'trips/:id', component: TripDetailsComponent }
    

]