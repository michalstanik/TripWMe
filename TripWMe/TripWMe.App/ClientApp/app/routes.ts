import { Routes } from '@angular/router'

import {
    TripDetailsComponent,
    TripsSummaryComponent
    
} from './trips/index';

export const appRoutes: Routes = [
    { path: '', component: TripsSummaryComponent},
    { path: 'trips/:id', component: TripDetailsComponent }
    

]