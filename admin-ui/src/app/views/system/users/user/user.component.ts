import { Component, OnDestroy, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { DialogService, DynamicDialogComponent } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { ChangeEmailComponent } from '../change-email/change-email.component';
import { RoleAssignComponent } from '../role-assign/role-assign.component';
import { SetPasswordComponent } from '../set-password/set-password.component';
import { UserDetailComponent } from '../user-detail/user-detail.component';
import {
    AdminApiUserApiClient,
    UserDto,
    UserDtoPagedResult,
} from 'src/app/api/admin-api.service.generated';
import { AlertService } from 'src/app/shared/services/alert.service';
import { MessageConstants } from 'src/app/shared/constants/messages.constant';

@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
})
export class UserComponent implements OnInit, OnDestroy {
    //System variables
    private ngUnsubscribe = new Subject<void>();
    public blockedPanel: boolean = false;

    //Paging variables
    public pageIndex: number = 1;
    public pageSize: number = 10;
    public totalCount: number;

    //Business variables
    public users: UserDto[];
    public selectedUsers: UserDto[] = [];
    public keyword: string = '';

    constructor(
        private userService: AdminApiUserApiClient,
        public dialogService: DialogService,
        private alertService: AlertService,
        private confirmationService: ConfirmationService
    ) {}

    ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    ngOnInit() {
        this.loadData();
    }

    loadData(selectionId = null) {
        this.toggleBlockUI(true);
        this.userService
            .getAllUsersPaged(this.keyword, this.pageIndex, this.pageSize)
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe({
                next: (response: UserDtoPagedResult) => {
                    this.users = response.results;
                    this.totalCount = response.rowCount;
                    if (selectionId != null && this.users.length > 0) {
                        this.selectedUsers = this.users.filter(
                            (x) => x.id == selectionId
                        );
                    }

                    this.toggleBlockUI(false);
                },
                error: () => {
                    this.toggleBlockUI(false);
                },
            });
    }

    showAddUserModal() {
        const ref = this.dialogService.open(UserDetailComponent, {
            header: 'Thêm mới người dùng',
            width: '70%',
        });
        const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
        const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
        const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
        dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
        ref.onClose.subscribe((data: UserDto) => {
            if (data) {
                this.alertService.showSuccess(MessageConstants.CREATED_OK_MSG);
                this.selectedUsers = [];
                this.loadData();
            }
        });
    }

    onPageChange(event: any): void {
        this.pageIndex = event.page + 1;
        this.pageSize = event.rows;
        this.loadData();
    }

    showEditUserModal(user: UserDto) {
        const ref = this.dialogService.open(UserDetailComponent, {
            data: {
                id: user.id,
            },
            header: 'Cập nhật người dùng',
            width: '70%',
        });
        const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
        const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
        const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
        dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
        ref.onClose.subscribe((data: UserDto) => {
            if (data) {
                this.alertService.showSuccess(MessageConstants.UPDATED_OK_MSG);
                this.selectedUsers = [];
                this.loadData(data.id);
            }
        });
    }

    deleteUsers() {
        if (this.selectedUsers.length == 0) {
            this.alertService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
            return;
        }
        var ids = [];
        this.selectedUsers.forEach((element) => {
            ids.push(element.id);
        });
        this.openDeleteUserConfirm(ids);
    }

    deleteUser(user: UserDto) {
        var ids = [user.id];
        this.openDeleteUserConfirm(ids);
    }

    deleteUsersConfirm(ids: any[]) {
        this.toggleBlockUI(true);
        this.userService.deleteUser(ids).subscribe({
            next: () => {
                this.alertService.showSuccess(MessageConstants.DELETED_OK_MSG);
                this.loadData();
                this.selectedUsers = [];
                this.toggleBlockUI(false);
            },
            error: () => {
                this.toggleBlockUI(false);
            },
        });
    }

    openDeleteUserConfirm(ids: any) {
        this.confirmationService.confirm({
            message: MessageConstants.CONFIRM_DELETE_MSG,
            accept: () => {
                this.deleteUsersConfirm(ids);
            },
        });
    }

    setPassword(id: string) {
        const ref = this.dialogService.open(SetPasswordComponent, {
            data: {
                id: id,
            },
            header: 'Đặt lại mật khẩu',
            width: '70%',
        });
        const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
        const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
        const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
        dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
        ref.onClose.subscribe((result: boolean) => {
            if (result) {
                this.alertService.showSuccess(
                    MessageConstants.CHANGE_PASSWORD_SUCCCESS_MSG
                );
                this.selectedUsers = [];
                this.loadData();
            }
        });
    }
    changeEmail(id: string) {
        const ref = this.dialogService.open(ChangeEmailComponent, {
            data: {
                id: id,
            },
            header: 'Đặt lại email',
            width: '70%',
        });
        const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
        const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
        const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
        dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
        ref.onClose.subscribe((result: boolean) => {
            if (result) {
                this.alertService.showSuccess(
                    MessageConstants.CHANGE_EMAIL_SUCCCESS_MSG
                );
                this.selectedUsers = [];
                this.loadData();
            }
        });
    }

    assignRole(id: string) {
        const ref = this.dialogService.open(RoleAssignComponent, {
            data: {
                id: id,
            },
            header: 'Gán quyền',
            width: '70%',
        });
        const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
        const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
        const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
        dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
        ref.onClose.subscribe((result: boolean) => {
            if (result) {
                this.alertService.showSuccess(
                    MessageConstants.ROLE_ASSIGN_SUCCESS_MSG
                );
                this.loadData();
            }
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
}
