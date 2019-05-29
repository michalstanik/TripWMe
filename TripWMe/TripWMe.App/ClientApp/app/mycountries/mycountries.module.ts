import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MDBBootstrapModulesPro } from 'ng-uikit-pro-standard';
import { DxVectorMapModule } from 'devextreme-angular';

import { routing } from './mycountries.routing';;

//Components
import { RootComponent } from './root/root.component';
import { MyCountryListComponent } from './my-country-list/my-country-list.component';
import { MyCountryThumbnailComponent } from './my-country-thumbnail/my-country-thumbnail.component';
import { MyCountryMapComponent } from './my-country-map/my-country-map.component';
import { MyCountryService } from './shared/services/mycountry.service';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        routing,
        MDBBootstrapModulesPro.forRoot(),
        DxVectorMapModule
    ],
    declarations: [
        RootComponent,
        MyCountryListComponent,
        MyCountryThumbnailComponent,
        MyCountryMapComponent
    ],
    exports: [],
    providers: [MyCountryService]
})

export class MyCountriesModule { }