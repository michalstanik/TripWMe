import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";

import { BaseService } from "./base.service";


@Injectable()

export class UserService extends BaseService {

    private loggedIn = false;

    constructor(private http: HttpClient) {
        super();
        this.loggedIn = !!localStorage.getItem('auth_token');
    }

    login(userName, password) {


    }
  
}

