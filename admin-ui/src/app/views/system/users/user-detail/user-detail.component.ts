import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import {
    Validators,
    FormControl,
    FormGroup,
    FormBuilder,
} from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { forkJoin, Subject, takeUntil } from 'rxjs';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { formatDate } from '@angular/common';
import {
    AdminApiRoleApiClient,
    AdminApiUserApiClient,
    RoleDto,
    UpdateUserDto,
    UserDto,
} from 'src/app/api/admin-api.service.generated';
import { UploadService } from 'src/app/shared/services/upload.service';
import { environment } from 'src/environments/environment';
@Component({
    templateUrl: 'user-detail.component.html',
})
export class UserDetailComponent implements OnInit, OnDestroy {
    private ngUnsubscribe = new Subject<void>();

    // Default
    public blockedPanelDetail: boolean = false;
    public form: FormGroup;
    public title: string;
    public btnDisabled = false;
    public saveBtnName: string;
    public roles: any[] = [];
    selectedUser = {} as UserDto;
    public avatarImage;

    formSavedEventEmitter: EventEmitter<any> = new EventEmitter();

    constructor(
        public ref: DynamicDialogRef,
        public config: DynamicDialogConfig,
        private roleService: AdminApiRoleApiClient,
        private userService: AdminApiUserApiClient,
        private utilService: UtilityService,
        private fb: FormBuilder,
        private uploadService: UploadService
    ) {}
    ngOnDestroy(): void {
        if (this.ref) {
            this.ref.close();
        }
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
    // Validate
    noSpecial: RegExp = /^[^<>*!_~]+$/;
    validationMessages = {
        fullName: [{ type: 'required', message: 'Bạn phải nhập tên' }],
        email: [{ type: 'required', message: 'Bạn phải nhập email' }],
        userName: [{ type: 'required', message: 'Bạn phải nhập tài khoản' }],
        password: [
            { type: 'required', message: 'Bạn phải nhập mật khẩu' },
            {
                type: 'pattern',
                message:
                    'Mật khẩu ít nhất 8 ký tự, ít nhất 1 số, 1 ký tự đặc biệt, và một chữ hoa',
            },
        ],
        phoneNumber: [
            { type: 'required', message: 'Bạn phải nhập số điện thoại' },
        ],
    };

    ngOnInit() {
        //Init form
        this.buildForm();
        //Load data to form
        var roles = this.roleService.getAllRoles();
        this.toggleBlockUI(true);
        forkJoin({
            roles,
        })
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe({
                next: (repsonse: any) => {
                    //Push categories to dropdown list
                    var roles = repsonse.roles as RoleDto[];
                    roles.forEach((element) => {
                        this.roles.push({
                            value: element.id,
                            label: element.name,
                        });
                    });

                    if (
                        this.utilService.isEmpty(this.config.data?.id) == false
                    ) {
                        this.loadFormDetails(this.config.data?.id);
                    } else {
                        this.setMode('create');
                        this.toggleBlockUI(false);
                    }
                },
                error: () => {
                    this.toggleBlockUI(false);
                },
            });
    }
    loadFormDetails(id: number) {
        this.userService
            .getUserById(id)
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe({
                next: (response: UserDto) => {
                    this.selectedUser = response;
                    this.buildForm();
                    this.setMode('update');

                    this.toggleBlockUI(false);
                },
                error: () => {
                    this.toggleBlockUI(false);
                },
            });
    }

    onFileChange(event) {
        if (event.target.files && event.target.files.length) {
            this.uploadService
                .uploadImage('users', event.target.files)
                .subscribe({
                    next: (response: any) => {
                        this.form.controls['avatar'].setValue(response.path);
                        this.avatarImage = environment.API_URL + response.path;
                    },
                    error: (err: any) => {
                        console.log(err);
                    },
                });
        }
    }

    saveChange() {
        this.toggleBlockUI(true);
        this.saveData();
    }

    private saveData() {
        this.toggleBlockUI(true);
        if (this.utilService.isEmpty(this.config.data?.id)) {
            this.userService
                .createUser(this.form.value)
                .pipe(takeUntil(this.ngUnsubscribe))
                .subscribe({
                    next: () => {
                        this.ref.close(this.form.value);
                        this.toggleBlockUI(false);
                    },
                    error: () => {
                        this.toggleBlockUI(false);
                    },
                });
        } else {
            this.userService
                .updateUser(
                    new UpdateUserDto({
                        ...this.form.value,
                        id: this.config.data?.id,
                        dob: new Date(this.form.value.dob),
                    })
                )
                .pipe(takeUntil(this.ngUnsubscribe))
                .subscribe({
                    next: () => {
                        this.toggleBlockUI(false);

                        this.ref.close(this.form.value);
                    },
                    error: () => {
                        this.toggleBlockUI(false);
                    },
                });
        }
    }
    private toggleBlockUI(enabled: boolean) {
        if (enabled == true) {
            this.btnDisabled = true;
            this.blockedPanelDetail = true;
        } else {
            setTimeout(() => {
                this.btnDisabled = false;
                this.blockedPanelDetail = false;
            }, 1000);
        }
    }

    setMode(mode: string) {
        if (mode == 'update') {
            this.form.controls['userName'].clearValidators();
            this.form.controls['userName'].disable();
            this.form.controls['email'].clearValidators();
            this.form.controls['email'].disable();
            this.form.controls['password'].clearValidators();
            this.form.controls['password'].disable();
        } else if (mode == 'create') {
            this.form.controls['userName'].addValidators(Validators.required);
            this.form.controls['userName'].enable();
            this.form.controls['email'].addValidators(Validators.required);
            this.form.controls['email'].enable();
            this.form.controls['password'].addValidators(Validators.required);
            this.form.controls['password'].enable();
        }
    }
    buildForm() {
        this.form = this.fb.group({
            firstName: new FormControl(
                this.selectedUser.firstName || null,
                Validators.required
            ),
            lastName: new FormControl(
                this.selectedUser.lastName || null,
                Validators.required
            ),
            userName: new FormControl(
                this.selectedUser.userName || null,
                Validators.required
            ),
            email: new FormControl(
                this.selectedUser.email || null,
                Validators.required
            ),
            phoneNumber: new FormControl(
                this.selectedUser.phoneNumber || null,
                Validators.required
            ),
            password: new FormControl(
                null,
                Validators.compose([
                    Validators.required,
                    Validators.pattern(
                        '^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-zd$@$!%*?&].{8,}$'
                    ),
                ])
            ),
            dob: new FormControl(
                this.selectedUser.dob
                    ? formatDate(this.selectedUser.dob, 'yyyy-MM-dd', 'en')
                    : null
            ),
            avatar: new FormControl(this.selectedUser.avatar || null),
            isActive: new FormControl(this.selectedUser.isActive || true),
        });

        if (this.selectedUser.avatar) {
            this.avatarImage = environment.API_URL + this.selectedUser.avatar;
        }
    }
}
