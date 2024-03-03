import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { NotfoundComponent } from './views/notfound/notfound.component';
import { AppLayoutComponent } from './layout/app.layout.component';

const routes: Routes = [
    {
        path: '',
        component: AppLayoutComponent,
        children: [
            {
                path: '',
                loadChildren: () =>
                    import('./views/dashboard/dashboard.module').then(
                        (m) => m.DashboardModule
                    ),
            },
            {
                path: 'content',
                loadChildren: () =>
                    import('./views/content/content.module').then(
                        (m) => m.ContentModule
                    ),
            },
            {
                path: 'system',
                loadChildren: () =>
                    import('./views/system/system.module').then(
                        (m) => m.SystemModule
                    ),
            },
        ],
    },
    {
        path: 'auth',
        loadChildren: () =>
            import('./views/auth/auth.module').then((m) => m.AuthModule),
    },
    { path: 'notfound', component: NotfoundComponent },
    { path: '**', redirectTo: '/notfound' },
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes, {
            scrollPositionRestoration: 'enabled',
            anchorScrolling: 'enabled',
            onSameUrlNavigation: 'reload',
        }),
    ],
    exports: [RouterModule],
})
export class AppRoutingModule {}
