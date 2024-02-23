import { INavData } from '@coreui/angular';
import { UrlConstants } from 'src/app/shared/constants/url.constants';

export const navItems: INavData[] = [
  {
    name: 'Trang chủ',
    url: UrlConstants.DASHBOARD,
    iconComponent: { name: 'cil-speedometer' },
    badge: {
      color: 'info',
      text: 'NEW',
    },
  },

  {
    name: 'Nội dung',
    url: UrlConstants.CONTENT,
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Danh mục',
        url: UrlConstants.POST_CATEGORIES,
      },
      {
        name: 'Bài viết',
        url: UrlConstants.POSTS,
      },
      {
        name: 'Loạt bài',
        url: UrlConstants.SERIES,
      },
      {
        name: 'Nhuận bút',
        url: UrlConstants.ROYALTY,
      },
    ],
  },
  {
    name: 'Hệ thống',
    url: UrlConstants.SYSTEM,
    iconComponent: { name: 'cil-cursor' },
    children: [
      {
        name: 'Quyền',
        url: UrlConstants.ROLES,
      },
      {
        name: 'Người dùng',
        url: UrlConstants.USERS,
      },
    ],
  },
];
