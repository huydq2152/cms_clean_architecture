import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html',
})
export class AppMenuComponent implements OnInit {
    model: any[] = [];

    constructor(public layoutService: LayoutService) {}

    ngOnInit() {
        this.model = [
            {
                label: 'Home',
                items: [
                    {
                        label: 'Trang chủ',
                        icon: 'pi pi-fw pi-home',
                        routerLink: ['/'],
                    },
                ],
            },
            {
                label: 'Content',
                icon: 'pi pi-fw pi-briefcase',
                items: [
                    {
                        label: 'Loại bài viết',
                        icon: 'pi pi-fw pi-circle',
                        routerLink: ['/content/post-categories'],
                    },
                    {
                        label: 'Bài viết',
                        icon: 'pi pi-fw pi-circle',
                        routerLink: ['/content/posts'],
                    },
                ],
            },
            {
                label: 'System',
                icon: 'pi pi-fw pi-briefcase',
                items: [
                    {
                        label: 'Người dùng',
                        icon: 'pi pi-fw pi-circle',
                        routerLink: ['/system/users'],
                    },
                    {
                        label: 'Vai trò',
                        icon: 'pi pi-fw pi-circle',
                        routerLink: ['/system/roles'],
                    },
                ],
            },
        ];
    }
}
