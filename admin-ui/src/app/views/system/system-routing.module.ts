import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './users/user/user.component';
import { RoleComponent } from './roles/role/role.component';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'users',
    pathMatch: 'full',
  },
  {
    path: 'users',
    component: UserComponent,
    data: {
      title: 'Users',
      requiredPolicy: 'Permissions.Users.View',
    },
    canActivate: [AuthGuardService],
  },
  {
    path: 'roles',
    component: RoleComponent,
    data: {
      title: 'Roles',
      requiredPolicy: 'Permissions.Roles.View',
    },
    canActivate: [AuthGuardService],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SystemRoutingModule {}
