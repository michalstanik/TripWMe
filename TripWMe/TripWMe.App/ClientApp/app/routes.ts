import { Routes } from '@angular/router'

import {
    TripDetailsComponent,
    TripsSummaryComponent,
    TripDashboardComponent
    
} from './trips/index';

export const appRoutes: Routes = [
    { path: '', component: TripDashboardComponent},
    { path: 'trips/:id', component: TripDetailsComponent }
    

]