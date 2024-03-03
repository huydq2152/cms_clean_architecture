import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login/login.component';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { AlertService } from 'src/app/shared/services/alert.service';
import { AdminApiAuthApiClient } from 'src/app/api/admin-api.service.generated';
import { BlockUIModule } from 'primeng/blockui';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@NgModule({
    imports: [
        CommonModule,
        AuthRoutingModule,
        ButtonModule,
        CheckboxModule,
        InputTextModule,
        FormsModule,
        PasswordModule,
        ReactiveFormsModule,
        BlockUIModule,
        ProgressSpinnerModule,
    ],
    declarations: [LoginComponent],
    providers: [AlertService, AdminApiAuthApiClient],
})
export class AuthModule {}
