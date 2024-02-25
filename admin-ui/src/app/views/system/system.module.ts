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
import { RolesDetailComponent } from './roles/role-detail/role-detail.component';
import { BlogSharedModule } from 'src/app/shared/modules/shared.module';
import { KeyFilterModule } from 'primeng/keyfilter';
import { PermissionGrantComponent } from './roles/permmision-grant/permission-grant.component';
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
  ],
  declarations: [
    UserComponent,
    RoleComponent,
    RolesDetailComponent,
    PermissionGrantComponent,
  ],
})
export class SystemModule {}
