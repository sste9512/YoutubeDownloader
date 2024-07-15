import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';

import {ModalModule} from 'ngx-bootstrap/modal';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {CounterComponent} from './counter/counter.component';
import {TokenComponent} from './token/token.component';

import {ApiAuthorizationModule} from 'src/api-authorization/api-authorization.module';
import {AuthorizeInterceptor} from 'src/api-authorization/authorize.interceptor';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatCardModule} from "@angular/material/card";
import {MatButtonModule} from "@angular/material/button";
import {MatInputModule} from "@angular/material/input";
import {VideoClient} from "./web-api-client";
import {IVideoClient} from "./web-api-client";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        TokenComponent,
    ],
    imports: [
        BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
        HttpClientModule,
        FormsModule,
        ApiAuthorizationModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        ModalModule.forRoot(),
        MatSidenavModule,
        MatCardModule,
        MatButtonModule,
        MatInputModule,
    ],
    providers: [
        {provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true},
         VideoClient,
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
