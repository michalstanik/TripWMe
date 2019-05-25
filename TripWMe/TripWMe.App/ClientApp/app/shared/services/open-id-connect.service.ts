import { Injectable } from '@angular/core';
import { UserManager, User } from 'oidc-client';

import { environment } from '../../../environments/environment';
import { ReplaySubject } from 'rxjs';

@Injectable()
export class OpenIdConnectService {

    private userManager: UserManager = new UserManager(environment.openIdConnectSettings);
    private currentUser: User;

    userLoaded$ = new ReplaySubject<boolean>(1);

    get userAvailable(): boolean {
        return this.currentUser != null;
    }

    get user(): User {
        return this.currentUser;
    }

    constructor() {
        this.userManager.clearStaleState();

        this.userManager.events.addUserLoaded(user => {
            if (!environment.production) {
                console.log('User loaded.', user);
            }
            this.currentUser = user;
            console.log('!!!Current user', this.currentUser);
            this.userLoaded$.next(true);
        });

        this.userManager.events.addUserUnloaded((e) => {
            if (!environment.production) {
                console.log('User unloaded');
            }
            this.currentUser = null;
            this.userLoaded$.next(false);
        });
    }

    triggerSignIn() {
        this.userManager.signinRedirect().then(function () {
            if (!environment.production) {
                console.log('Redirection to signin triggered.');
            }
        });
    }

    handleCallback() {
        this.userManager.signinRedirectCallback().then(function (user) {
            if (!environment.production) {
                console.log('Callback after signin handled.', user);
            }
        });
    }

}
