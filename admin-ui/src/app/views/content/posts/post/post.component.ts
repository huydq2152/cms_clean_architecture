import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { DialogService, DynamicDialogComponent } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { MessageConstants } from 'src/app/shared/constants/messages.constant';
import { PostDetailComponent } from '../post-detail/post-detail.component';
import {
    AdminApiBlogApiClient,
    AdminApiPostApiClient,
    GetAllPostsInput,
    PostCategoryDto,
    PostDto,
    PostDtoPagedResult,
    UserDto,
} from 'src/app/api/admin-api.service.generated';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
    selector: 'app-post',
    templateUrl: './post.component.html',
})
export class PostComponent implements OnInit, OnDestroy {
    //System variables
    private ngUnsubscribe = new Subject<void>();
    public blockedPanel: boolean = false;

    //Paging variables
    public pageIndex: number = 1;
    public pageSize: number = 10;
    public totalCount: number;

    //Business variables
    public posts: PostDto[] = [];
    public selectedPosts: PostDto[] = [];
    public keyword: string = '';

    //Filter variables
    filteredPostCategories: PostCategoryDto[] = [];
    selectedPostCategory: PostCategoryDto = null;
    filteredUsers: UserDto[] = [];
    selectedUser: UserDto = null;

    constructor(
        private postService: AdminApiPostApiClient,
        public dialogService: DialogService,
        private alertService: AlertService,
        private confirmationService: ConfirmationService,
        private blogService: AdminApiBlogApiClient
    ) {}

    ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.toggleBlockUI(true);
        this.postService
            .getAllPostPaged(
                new GetAllPostsInput({
                    pageIndex: this.pageIndex,
                    pageSize: this.pageSize,
                    keyword: this.keyword,
                    categoryId: this.selectedPostCategory?.id,
                    authorUserId: this.selectedUser?.id,
                })
            )
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe({
                next: (response: PostDtoPagedResult) => {
                    this.posts = response.results;
                    this.totalCount = response.rowCount;

                    this.toggleBlockUI(false);
                },
                error: () => {
                    this.toggleBlockUI(false);
                },
            });
    }

    showAddPostModal() {
        const ref = this.dialogService.open(PostDetailComponent, {
            header: 'Thêm mới bài viết',
            width: '70%',
        });
        const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
        const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
        const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
        dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
        ref.onClose.subscribe((data: PostDto) => {
            if (data) {
                this.alertService.showSuccess(MessageConstants.CREATED_OK_MSG);
                this.selectedPosts = [];
                this.loadData();
            }
        });
    }

    onPageChange(event): void {
        this.pageIndex = event.page + 1;
        this.pageSize = event.rows;
        this.loadData();
    }

    showEditPostModal(post: PostDto) {
        const ref = this.dialogService.open(PostDetailComponent, {
            data: {
                id: post.id,
            },
            header: 'Cập nhật bài viết',
            width: '70%',
        });
        const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
        const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
        const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
        dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
        ref.onClose.subscribe((data: PostDto) => {
            if (data) {
                this.alertService.showSuccess(MessageConstants.UPDATED_OK_MSG);
                this.selectedPosts = [];
                this.loadData();
            }
        });
    }

    deletePosts() {
        if (this.selectedPosts.length == 0) {
            this.alertService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
            return;
        }
        var ids = [];
        this.selectedPosts.forEach((element) => {
            ids.push(element.id);
        });
        this.openDeletePostConfirm(ids);
    }

    deletePost(post: PostDto) {
        var ids = [post.id];
        this.openDeletePostConfirm(ids);
    }

    openDeletePostConfirm(ids: any[]) {
        this.confirmationService.confirm({
            message: MessageConstants.CONFIRM_DELETE_MSG,
            accept: () => {
                this.deletePostsConfirm(ids);
            },
        });
    }

    deletePostsConfirm(ids: any[]) {
        this.toggleBlockUI(true);

        this.postService.deletePost(ids).subscribe({
            next: () => {
                this.alertService.showSuccess(MessageConstants.DELETED_OK_MSG);
                this.loadData();
                this.selectedPosts = [];
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

    filterUsers(event): void {
        var filter = event.query;
        this.blogService
            .getAllBlogUsers(filter)
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe((res) => {
                if (res.length !== 0) {
                    this.filteredUsers = res;
                }
            });
    }

    public selectedUserDisplay(user: UserDto): string {
        return `${user.userName}`;
    }
}
