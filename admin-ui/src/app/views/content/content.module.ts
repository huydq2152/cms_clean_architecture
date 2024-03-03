import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentRoutingModule } from './content-routing.module';
import { FormsModule } from '@angular/forms';
import { PostCategoryComponent } from './post-categories/post-category/post-category.component';
import { PostCategoryDetailComponent } from './post-categories/post-category-detail/post-category-detail.component';
import { TableModule } from 'primeng/table';
import { FileUploadModule } from 'primeng/fileupload';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { RatingModule } from 'primeng/rating';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { DropdownModule } from 'primeng/dropdown';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputNumberModule } from 'primeng/inputnumber';
import { DialogModule } from 'primeng/dialog';
import { BlockUIModule } from 'primeng/blockui';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { PaginatorModule } from 'primeng/paginator';

import { PanelModule } from 'primeng/panel';
import { BlogSharedModule } from 'src/app/shared/modules/shared.module';
import { KeyFilterModule } from 'primeng/keyfilter';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CheckboxModule } from 'primeng/checkbox';
import { ReactiveFormsModule } from '@angular/forms';
import { PostComponent } from './posts/post/post.component';
import { PostDetailComponent } from './posts/post-detail/post-detail.component';
import { EditorModule } from 'primeng/editor';
import { BadgeModule } from 'primeng/badge';

@NgModule({
    declarations: [
        PostCategoryComponent,
        PostCategoryDetailComponent,
        PostComponent,
        PostDetailComponent,
    ],
    imports: [
        CommonModule,
        ContentRoutingModule,
        TableModule,
        FileUploadModule,
        FormsModule,
        ButtonModule,
        RippleModule,
        ToastModule,
        ToolbarModule,
        RatingModule,
        InputTextModule,
        InputTextareaModule,
        DropdownModule,
        RadioButtonModule,
        InputNumberModule,
        DialogModule,
        BlockUIModule,
        ProgressSpinnerModule,
        PaginatorModule,
        PanelModule,
        BlogSharedModule,
        KeyFilterModule,
        AutoCompleteModule,
        CheckboxModule,
        ReactiveFormsModule,
        EditorModule,
        BadgeModule,
    ],
})
export class ContentModule {}
