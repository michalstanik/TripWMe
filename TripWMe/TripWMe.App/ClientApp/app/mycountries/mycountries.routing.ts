import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';

import { RootComponent } from './root/root.component';

export const routing: ModuleWithProviders = RouterModule.forChild([
    {
        path: 'mycountries',
        component: RootComponent,

        children: [

        ]
    }
]);

