import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';

import { RootComponent } from './root/root.component';
import { TripsSummaryComponent } from './trips/trips-summary/trips-summary.component';
import { TripDetailsComponent } from './trips/trip-details/trip-details.component';

export const routing: ModuleWithProviders = RouterModule.forChild([
    {
        path: 'dashboard',
        component: RootComponent,

        children: [
            { path: '', component: TripsSummaryComponent },
            { path: 'trips/:id', component: TripDetailsComponent }
        ]
    }
]);

