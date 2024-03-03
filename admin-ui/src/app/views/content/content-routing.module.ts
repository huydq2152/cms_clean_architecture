import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PostCategoryComponent } from './post-categories/post-category/post-category.component';
import { AuthGuardService } from '../../shared/services/auth-guard.service';
import { PostComponent } from './posts/post/post.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'posts',
        pathMatch: 'full',
    },
    {
        path: 'post-categories',
        component: PostCategoryComponent,
        data: {
            title: 'post categories',
            requiredPolicy: 'Permissions.PostCategories.View',
        },
        canActivate: [AuthGuardService],
    },
    {
        path: 'posts',
        component: PostComponent,
        data: {
            title: 'posts',
            requiredPolicy: 'Permissions.Posts.View',
        },
        canActivate: [AuthGuardService],
    },
    { path: '**', redirectTo: '/notfound' },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ContentRoutingModule {}
