import { Component, OnInit } from '@angular/core';
import { OpenIdConnectService } from 'ClientApp/app/shared/services/open-id-connect.service';
import { Router } from '@angular/router';
import { environment } from 'ClientApp/environments/environment';

@Component({
  selector: 'app-signin-oidc',
  templateUrl: './signin-oidc.component.html',
  styleUrls: ['./signin-oidc.component.scss']
})
export class SigninOidcComponent implements OnInit {

    constructor(private openIdConnectService: OpenIdConnectService,
        private router: Router) { }

    ngOnInit() {
        console.log("SigninOidcComponent OnInit");
        //this.openIdConnectService.userLoaded$.subscribe((userLoaded) => {
        //    if (userLoaded) {
        //        console.log("use loaded:", userLoaded);
        //        this.router.navigate(['./']);
        //    }
        //    else {
        //        if (!environment.production) {
        //            console.log("An error happened: user wasn't loaded.");
        //        }
        //    }
        //});

        this.openIdConnectService.handleCallback();
        this.router.navigate(['./']);
    }



}
