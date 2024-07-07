import { useMsal } from "@azure/msal-react";
import { Outlet, useNavigate } from "react-router-dom";

const AuthenticateUser = () => {
  const { accounts } = useMsal();
  const navigate = useNavigate();
  return accounts.length > 0 ? <Outlet /> : navigate("/login");
};

export default AuthenticateUser;
