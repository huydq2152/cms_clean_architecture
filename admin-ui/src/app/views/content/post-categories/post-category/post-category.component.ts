import { Component, OnDestroy, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService, DynamicDialogComponent } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import {
    AdminApiBlogApiClient,
    AdminApiPostCategoryApiClient,
    GetAllPostCategoriesInput,
    PostCategoryDto,
    PostCategoryDtoPagedResult,
} from 'src/app/api/admin-api.service.generated';
import { MessageConstants } from 'src/app/shared/constants/messages.constant';
import { AlertService } from 'src/app/shared/services/alert.service';
import { PostCategoryDetailComponent } from '../post-category-detail/post-category-detail.component';

@Component({
    templateUrl: './post-category.component.html',
    providers: [MessageService],
})
export class PostCategoryComponent implements OnInit, OnDestroy {
    //System variables
    private ngUnsubscribe = new Subject<void>();
    public blockedPanel: boolean = false;

    //Paging variables
    public pageIndex: number = 1;
    public pageSize: number = 10;
    public totalCount: number;

    //Business variables
    public postCategories: PostCategoryDto[];
    public selectedPostCategories: PostCategoryDto[] = [];
    public keyword: string = '';

    //Filter variables
    filteredPostCategories: PostCategoryDto[] = [];
    selectedPostCategory: PostCategoryDto = null;

    constructor(
        private postCategoryService: AdminApiPostCategoryApiClient,
        public dialogService: DialogService,
        private alertService: AlertService,
        private confirmationService: ConfirmationService,
        private blogService: AdminApiBlogApiClient
    ) {}

    ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
    ngOnInit(): void {
        this.loadData();
    }

    loadData() {
        this.toggleBlockUI(true);

        this.postCategoryService
            .getAllPostCategoryPaged(
                new GetAllPostCategoriesInput({
                    pageIndex: this.pageIndex,
                    pageSize: this.pageSize,
                    keyword: this.keyword,
                    parentId: this.selectedPostCategory?.id,
                })
            )
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe({
                next: (response: PostCategoryDtoPagedResult) => {
                    this.postCategories = response.results;
                    this.totalCount = response.rowCount;

                    this.toggleBlockUI(false);
                },
                error: () => {
                    this.toggleBlockUI(false);
                },
            });
    }

    onPageChange(event): void {
        this.pageIndex = event.page + 1;
        this.pageSize = event.rows;
        this.loadData();
    }

    showAddPostCategoryModal() {
        const ref = this.dialogService.open(PostCategoryDetailComponent, {
            header: 'Thêm mới loại bài viết',
            width: '70%',
        });
        const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
        const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
        const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
        dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
        ref.onClose.subscribe((data: PostCategoryDto) => {
            if (data) {
                this.alertService.showSuccess(MessageConstants.CREATED_OK_MSG);
                this.selectedPostCategories = [];
                this.loadData();
            }
        });
    }

    showEditPostCategoryModal(postCategory: PostCategoryDto) {
        const ref = this.dialogService.open(PostCategoryDetailComponent, {
            data: {
                id: postCategory.id,
            },
            header: 'Cập nhật loại bài viết',
            width: '70%',
        });
        const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
        const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
        const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
        dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
        ref.onClose.subscribe((data: PostCategoryDto) => {
            if (data) {
                this.alertService.showSuccess(MessageConstants.UPDATED_OK_MSG);
                this.selectedPostCategories = [];
                this.loadData();
            }
        });
    }

    deletePostCategories() {
        if (this.selectedPostCategories.length == 0) {
            this.alertService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
            return;
        }
        var ids = [];
        this.selectedPostCategories.forEach((element) => {
            ids.push(element.id);
        });
        this.openDeletePostCategoriesConfirm(ids);
    }

    deletePostCategory(postCategory: PostCategoryDto) {
        var ids = [postCategory.id];
        this.openDeletePostCategoriesConfirm(ids);
    }

    openDeletePostCategoriesConfirm(ids: any[]) {
        this.confirmationService.confirm({
            message: MessageConstants.CONFIRM_DELETE_MSG,
            accept: () => {
                this.deletePostCategoriesConfirm(ids);
            },
        });
    }

    deletePostCategoriesConfirm(ids: any[]) {
        this.toggleBlockUI(true);

        this.postCategoryService.deletePostCategory(ids).subscribe({
            next: () => {
                this.alertService.showSuccess(MessageConstants.DELETED_OK_MSG);
                this.loadData();
                this.selectedPostCategories = [];
                this.toggleBlockUI(false);
            },
            error: () => {
                this.toggleBlockUI(false);
            },
        });
    }

    private toggleBlockUI(enabled: boolean) {
        if (enabled == true) {
            this.blockedPanel = true;
        } else {
            setTimeout(() => {
                this.blockedPanel = false;
            }, 1000);
        }
    }

    filterPostCategories(event): void {
        var filter = event.query;
        this.blogService
            .getAllBlogPostCategories(filter)
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe((res) => {
                if (res.length !== 0) {
                    this.filteredPostCategories = res;
                }
            });
    }

    public selectedPostCategoryDisplay(postCategory: PostCategoryDto): string {
        return `${postCategory.code} - ${postCategory.name}`;
    }
}
