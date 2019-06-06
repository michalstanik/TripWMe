import { Component } from '@angular/core';
import { OpenIdConnectService } from './shared/services/open-id-connect.service';
import { MDBSpinningPreloader } from 'ng-uikit-pro-standard';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'Trip With Me';

    constructor(private openIdConnectService: OpenIdConnectService, private mdbSpinningPreloader: MDBSpinningPreloader) {
    }

    ngOnInit() {
        console.log('ngOnInit: app-component');
        this.mdbSpinningPreloader.stop();

        this.openIdConnectService.getUser();
        console.log("Path 1", window.location.pathname);
        window.location.hash = decodeURIComponent(window.location.hash);

        console.log("Path 2", window.location.pathname);
        var path = window.location.pathname;
        if (path != "/signin-oidc") {
            if (!this.openIdConnectService.userAvailable) {
                console.log('!!!!!!User Not avaialble');
               this.openIdConnectService.triggerSignIn();
            }

        }
    }

}