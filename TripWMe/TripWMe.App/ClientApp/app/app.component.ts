import { Component } from '@angular/core';
import { OpenIdConnectService } from './shared/services/open-id-connect.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'Trip With Me';

    constructor(private openIdConnectService: OpenIdConnectService) {
    }

    ngOnInit() {
        //window.location.hash = decodeURIComponent(window.location.hash);
        var path = window.location.pathname;
        if (path != "/signin-oidc") {
            if (!this.openIdConnectService.userAvailable) {
                console.log('!!!!!!User Not avaialble');
               this.openIdConnectService.triggerSignIn();
            }

        }
    }

}