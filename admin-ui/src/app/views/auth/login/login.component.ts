import { Component, OnDestroy, OnInit } from '@angular/core';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import {
    AdminApiAuthApiClient,
    AuthenticatedResult,
    LoginRequest,
} from 'src/app/api/admin-api.service.generated';
import { UrlConstants } from 'src/app/shared/constants/url.constants';
import { AlertService } from 'src/app/shared/services/alert.service';
import { TokenStorageService } from 'src/app/shared/services/token-storage.service';
import { BroadcastService } from 'src/app/shared/services/broadcast.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [
        `
            :host ::ng-deep .pi-eye,
            :host ::ng-deep .pi-eye-slash {
                transform: scale(1.6);
                margin-right: 1rem;
                color: var(--primary-color) !important;
            }
        `,
    ],
})
export class LoginComponent implements OnInit, OnDestroy {
    loginForm: FormGroup;
    public blockedPanel: boolean = false;
    private ngUnsubscribe = new Subject<void>();
    loading = false;

    constructor(
        public layoutService: LayoutService,
        private fb: FormBuilder,
        private authApiClient: AdminApiAuthApiClient,
        private alertService: AlertService,
        private router: Router,
        private tokenService: TokenStorageService,
        private broadcastService: BroadcastService
    ) {
        this.loginForm = this.fb.group({
            userName: new FormControl('', [Validators.required]),
            password: new FormControl('', [Validators.required]),
        });
    }
    ngOnInit(): void {
        this.broadcastService.httpError.asObservable().subscribe((res) => {
            if (res) {
                this.loading = false;
            }
        });
    }
    ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    login() {
        this.toggleBlockUI(true);
        var request: LoginRequest = new LoginRequest({
            userName: this.loginForm.value.userName,
            password: this.loginForm.value.password,
        });
        this.authApiClient
            .login(request)
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe({
                next: (res: AuthenticatedResult) => {
                    // Save token to local storage
                    if (res && res.token && res.refreshToken) {
                        this.tokenService.saveToken(res.token);
                        this.tokenService.saveRefreshToken(res.refreshToken);
                        this.tokenService.saveUser(res);
                    }

                    // Redirect to dashboard
                    this.toggleBlockUI(false);
                    this.router.navigate([UrlConstants.DASHBOARD]);
                },
                error: () => {
                    this.alertService.showError('Đăng nhập không đúng.');
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
}
