import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { SystemRoutingModule } from './system-routing.module';

import { UserComponent } from './users/user/user.component';
import { RoleComponent } from './roles/role/role.component';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { BlockUIModule } from 'primeng/blockui';
import { PaginatorModule } from 'primeng/paginator';
import { PanelModule } from 'primeng/panel';
import { CheckboxModule } from 'primeng/checkbox';
import { SharedModule } from 'primeng/api';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { RoleDetailComponent } from './roles/role-detail/role-detail.component';
import { BlogSharedModule } from 'src/app/shared/modules/shared.module';
import { KeyFilterModule } from 'primeng/keyfilter';
import { PermissionGrantComponent } from './roles/permmision-grant/permission-grant.component';
import { ChangeEmailComponent } from './users/change-email/change-email.component';
import { RoleAssignComponent } from './users/role-assign/role-assign.component';
import { SetPasswordComponent } from './users/set-password/set-password.component';
import { UserDetailComponent } from './users/user-detail/user-detail.component';
import { BadgeModule } from 'primeng/badge';
import { PickListModule } from 'primeng/picklist';
import { ImageModule } from 'primeng/image';
@NgModule({
  imports: [
    SystemRoutingModule,
    CommonModule,
    ReactiveFormsModule,
    TableModule,
    ProgressSpinnerModule,
    BlockUIModule,
    PaginatorModule,
    PanelModule,
    CheckboxModule,
    ButtonModule,
    InputTextModule,
    SharedModule,
    KeyFilterModule,
    SharedModule,
    BlogSharedModule,
    BadgeModule,
    PickListModule,
    ImageModule,
  ],
  declarations: [
    UserComponent,
    RoleComponent,
    RoleDetailComponent,
    PermissionGrantComponent,
    ChangeEmailComponent,
    RoleAssignComponent,
    SetPasswordComponent,
    UserDetailComponent,
  ],
})
export class SystemModule {}
