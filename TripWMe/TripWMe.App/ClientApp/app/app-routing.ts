import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { SigninOidcComponent } from './signin-oidc/signin-oidc.component';

const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'signin-oidc', component: SigninOidcComponent }

];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes, { useHash: true });