import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import {
    AdminApiRoleApiClient,
    GetAllRolesInput,
    RoleDto,
    RoleDtoPagedResult,
} from 'src/app/api/admin-api.service.generated';
import { DialogService, DynamicDialogComponent } from 'primeng/dynamicdialog';
import { AlertService } from 'src/app/shared/services/alert.service';
import { ConfirmationService } from 'primeng/api';
import { RoleDetailComponent } from '../role-detail/role-detail.component';
import { MessageConstants } from '../../../../shared/constants/messages.constant';
import { PermissionGrantComponent } from '../permmision-grant/permission-grant.component';

@Component({
    selector: 'app-role',
    templateUrl: './role.component.html',
})
export class RoleComponent implements OnInit, OnDestroy {
    //System variables
    private ngUnsubscribe = new Subject<void>();
    public blockedPanel: boolean = false;

    //Paging variables
    public pageIndex: number = 1;
    public pageSize: number = 10;
    public totalCount: number;

    //Business variables
    public roles: RoleDto[];
    public selectedRoles: RoleDto[] = [];
    public keyword: string = '';

    constructor(
        private roleService: AdminApiRoleApiClient,
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

    loadData() {
        this.toggleBlockUI(true);

        this.roleService
            .getAllRolesPaged(
                new GetAllRolesInput({
                    pageIndex: this.pageIndex,
                    pageSize: this.pageSize,
                    keyword: this.keyword,
                })
            )
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe({
                next: (response: RoleDtoPagedResult) => {
                    this.roles = response.results;
                    this.totalCount = response.rowCount;

                    this.toggleBlockUI(false);
                },
                error: (e) => {
                    this.toggleBlockUI(false);
                },
            });
    }

    onPageChange(event: any): void {
        this.pageIndex = event.page;
        this.pageSize = event.rows;
        this.loadData();
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
    showPermissionModal(id: string, name: string) {
        const ref = this.dialogService.open(PermissionGrantComponent, {
            data: {
                id: id,
            },
            header: name,
            width: '70%',
        });
        const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
        const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
        const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
        dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
        ref.onClose.subscribe((data: RoleDto) => {
            if (data) {
                this.alertService.showSuccess(MessageConstants.UPDATED_OK_MSG);
                this.selectedRoles = [];
                this.loadData();
            }
        });
    }
    showAddRoleModal() {
        const ref = this.dialogService.open(RoleDetailComponent, {
            header: 'Thêm mới quyền',
            width: '70%',
        });
        const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
        const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
        const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
        dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
        ref.onClose.subscribe((data: RoleDto) => {
            if (data) {
                this.alertService.showSuccess(MessageConstants.CREATED_OK_MSG);
                this.selectedRoles = [];
                this.loadData();
            }
        });
    }

    showEditRoleModal(role: RoleDto) {
        const ref = this.dialogService.open(RoleDetailComponent, {
            data: {
                id: role.id,
            },
            header: 'Cập nhật quyền',
            width: '70%',
        });
        const dialogRef = this.dialogService.dialogComponentRefMap.get(ref);
        const dynamicComponent = dialogRef?.instance as DynamicDialogComponent;
        const ariaLabelledBy = dynamicComponent.getAriaLabelledBy();
        dynamicComponent.getAriaLabelledBy = () => ariaLabelledBy;
        ref.onClose.subscribe((data: RoleDto) => {
            if (data) {
                this.alertService.showSuccess(MessageConstants.UPDATED_OK_MSG);
                this.selectedRoles = [];
                this.loadData();
            }
        });
    }

    deleteRoles() {
        if (this.selectedRoles.length == 0) {
            this.alertService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
            return;
        }
        var ids = [];
        this.selectedRoles.forEach((element) => {
            ids.push(element.id);
        });
        this.openDeleteRolesConfirm(ids);
    }

    deleteRole(role: RoleDto) {
        var ids = [role.id];
        this.openDeleteRolesConfirm(ids);
    }

    deleteRolesConfirm(ids: any[]) {
        this.toggleBlockUI(true);

        this.roleService.deleteRoles(ids).subscribe({
            next: () => {
                this.alertService.showSuccess(MessageConstants.DELETED_OK_MSG);
                this.loadData();
                this.selectedRoles = [];
                this.toggleBlockUI(false);
            },
            error: () => {
                this.toggleBlockUI(false);
            },
        });
    }

    openDeleteRolesConfirm(ids: any[]) {
        this.confirmationService.confirm({
            message: MessageConstants.CONFIRM_DELETE_MSG,
            accept: () => {
                this.deleteRolesConfirm(ids);
            },
        });
    }
}
