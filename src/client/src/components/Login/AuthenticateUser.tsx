import { useMsal } from "@azure/msal-react";
import { useEffect } from "react";
import { Outlet, useNavigate } from "react-router-dom";

const AuthenticateUser = () => {
  const { accounts } = useMsal();
  const navigate = useNavigate();

  useEffect(() => {
    if (accounts.length === 0) {
      navigate("/login");
    }
  }, [accounts, navigate]);

  return accounts.length > 0 ? <Outlet /> : null;
};

export default AuthenticateUser;
