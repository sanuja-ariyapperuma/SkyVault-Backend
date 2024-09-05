import {
  AuthenticatedTemplate,
  UnauthenticatedTemplate,
} from "@azure/msal-react";

import { Outlet } from "react-router-dom";
import Login from "../../pages/Login";

const AuthenticateUser = () => {
  return (
    <>
      <AuthenticatedTemplate>
        <Outlet />
      </AuthenticatedTemplate>
      <UnauthenticatedTemplate>
        <Login />
      </UnauthenticatedTemplate>
    </>
  );
};

export default AuthenticateUser;
