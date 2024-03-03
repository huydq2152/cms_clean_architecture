import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full',
    },
    {
        path: 'dashboard',
        component: DashboardComponent,
        data: {
            title: 'Dashboard',
            requiredPolicy: 'Permissions.Dashboard.View',
        },
        canActivate: [AuthGuardService],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class DashboardsRoutingModule {}
