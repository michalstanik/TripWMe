import { Injectable, EventEmitter } from '@angular/core';
import { UserManager, User } from 'oidc-client';

import { environment } from '../../../environments/environment';
import { ReplaySubject, Observable } from 'rxjs';



@Injectable()
export class OpenIdConnectService {

    private userManager: UserManager = new UserManager(environment.openIdConnectSettings);
    private currentUser: User;

    userLoaded$ = new ReplaySubject<boolean>(1);
    userLoadededEvent: EventEmitter<User> = new EventEmitter<User>();
    loggedIn = false;

    get userAvailable(): boolean {
        console.log("this.currentUser: ", this.currentUser);
        return this.currentUser != null;
    }



    get user(): User {
        return this.currentUser;
    }

    constructor() {
        console.log("Constructor: OpenIdConnectService");
        this.userManager.clearStaleState();

        this.userManager.getUser()
            .then((user) => {
                if (user) {
                    this.loggedIn = true;
                    this.currentUser = user;
                    this.userLoadededEvent.emit(user);
                    console.log("New mwthod:   ", this.currentUser);
                }
                else {
                    this.loggedIn = false;
                }
            })
            .catch((err) => {
                this.loggedIn = false;
            });

        this.userManager.events.addUserLoaded(user => {
            if (!environment.production) {
                console.log('User loaded.', user);
            }
            try {
                
                this.currentUser = user;
                console.log('!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Current user', this.currentUser);
            } catch (e) {
                console.log("Error with using user", e);
            }
            //this.currentUser = user;
            console.log('!!!Current user', this.currentUser);
            //this.userLoaded$.next(true);
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
        console.log("%%%%%%%%%%userAvailable: ", this.currentUser);
    }

    getUser() {
        this.userManager.getUser().then((user) => {
            this.currentUser = user;
            console.log('got user', user);
            this.userLoadededEvent.emit(user);
        }).catch(function (err) {
            console.log(err);
        });
    }

}
