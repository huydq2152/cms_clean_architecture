export interface UserModel {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  roles: string[];
  permissions: any;
}
