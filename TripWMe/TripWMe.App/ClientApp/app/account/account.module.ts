import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
//import { SharedModule } from '../shared/modules/shared.module';

//import { UserService } from '../shared/services/user.service';

//import { EmailValidator } from '../directives/email.validator.directive';

import { routing } from './account.routing';
import { LoginFormComponent } from './login-form/login-form.component';


@NgModule({
    imports: [
        CommonModule, FormsModule, routing
    ],
    declarations: [LoginFormComponent],
    providers: []
})
export class AccountModule { }
