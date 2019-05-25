import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { DxVectorMapModule } from 'devextreme-angular';
import { MDBBootstrapModulesPro } from 'ng-uikit-pro-standard';
import { MDBSpinningPreloader } from 'ng-uikit-pro-standard';


import { AppComponent } from './app.component';

import { routing } from "./app-routing";
import { EnsureAcceptHeaderInterceptor } from './shared/ensure-accept-header-interceptor';

import { AccountModule } from './account/account.module';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { DashboardModule } from './dashboard/dasboard.module';
import { OpenIdConnectService } from './shared/services/open-id-connect.service';
import { SigninOidcComponent } from './account/signin-oidc/signin-oidc.component';


@NgModule({
  declarations: [
      AppComponent,
      HeaderComponent,
      HomeComponent,
      SigninOidcComponent
  ],
    imports: [
        AccountModule,
        DashboardModule,
        BrowserModule,
        NgbModule,
        FormsModule,       
        HttpClientModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        DxVectorMapModule,
        MDBBootstrapModulesPro.forRoot(),
        routing
  ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: EnsureAcceptHeaderInterceptor,
            multi: true
        },
        MDBSpinningPreloader,
        OpenIdConnectService],
  bootstrap: [AppComponent]
})
export class AppModule { }
