import { NgModule } from '@angular/core';
import {
    PathLocationStrategy,
    LocationStrategy,
    HashLocationStrategy,
} from '@angular/common';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AppLayoutModule } from './layout/app.layout.module';
import { NotfoundComponent } from './views/notfound/notfound.component';
import { AuthGuardService } from './shared/services/auth-guard.service';
import { TokenInterceptor } from './shared/interceptors/token.interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TokenStorageService } from './shared/services/token-storage.service';
import { BroadcastService } from './shared/services/broadcast.service';

import { environment } from './../environments/environment';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import {
    ADMIN_API_BASE_URL,
    AdminApiAuthApiClient,
    AdminApiTokenApiClient,
    AdminApiRoleApiClient,
    AdminApiUserApiClient,
    AdminApiPostCategoryApiClient,
    AdminApiBlogApiClient,
    AdminApiPostApiClient,
} from './api/admin-api.service.generated';
import { DialogService } from 'primeng/dynamicdialog';
import { AlertService } from './shared/services/alert.service';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { UtilityService } from './shared/services/utility.service';

@NgModule({
    declarations: [AppComponent, NotfoundComponent],
    imports: [
        HttpClientModule,
        AppRoutingModule,
        AppLayoutModule,
        ToastModule,
        ConfirmDialogModule,
    ],
    providers: [
        { provide: ADMIN_API_BASE_URL, useValue: environment.API_URL },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: TokenInterceptor,
            multi: true,
        },
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        AdminApiAuthApiClient,
        AdminApiTokenApiClient,
        AdminApiRoleApiClient,
        AdminApiUserApiClient,
        AdminApiPostCategoryApiClient,
        AdminApiBlogApiClient,
        AdminApiPostApiClient,
        AuthGuardService,
        TokenStorageService,
        BroadcastService,
        MessageService,
        DialogService,
        AlertService,
        ConfirmationService,
        UtilityService,
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
