export enum UserRole {
  SuperAdmin = "SuperAdmin",
  Admin = "Admin",
  User = "User",
}

export type UserType = {
  id: number;
  displayName: string;
  role: UserRole;
};
