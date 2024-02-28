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
    attributes: {
      policyName: 'Permissions.Dashboard.View',
    },
  },

  {
    name: 'Nội dung',
    url: UrlConstants.CONTENT,
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Loại bài viết',
        url: UrlConstants.POST_CATEGORIES,
        attributes: {
          policyName: 'Permissions.PostCategories.View',
        },
      },
      {
        name: 'Bài viết',
        url: UrlConstants.POSTS,
        attributes: {
          policyName: 'Permissions.Posts.View',
        },
      },
      {
        name: 'Loạt bài',
        url: UrlConstants.SERIES,
        attributes: {
          policyName: 'Permissions.Series.View',
        },
      },
      {
        name: 'Nhuận bút',
        url: UrlConstants.ROYALTY,
        attributes: {
          policyName: 'Permissions.Royalty.View',
        },
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
        attributes: {
          policyName: 'Permissions.Roles.View',
        },
      },
      {
        name: 'Người dùng',
        url: UrlConstants.USERS,
        attributes: {
          policyName: 'Permissions.Users.View',
        },
      },
    ],
  },
];
