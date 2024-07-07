import localStyles from "../components/Login/Login.module.css";
import logo from "../assets/login_logo.png";
import LoginButton from "../components/Login/LoginButton";
import { useMsal } from "@azure/msal-react";
import {
  AuthError,
  BrowserAuthError,
  InteractionRequiredAuthError,
} from "@azure/msal-browser";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const { instance, accounts } = useMsal();
  const navigate = useNavigate();

  useEffect(() => {
    if (accounts.length > 0) {
      navigate("/");
    }
  });

  const loginToApp = async () => {
    try {
      await instance.loginPopup({
        scopes: ["user.read"],
      });
    } catch (error) {
      if (error instanceof InteractionRequiredAuthError) {
        console.error("Interaction required:", error.message);
      } else if (
        error instanceof BrowserAuthError &&
        error.errorCode === "user_cancelled"
      ) {
        console.error("User cancelled the login flow.");
      } else if (error instanceof AuthError) {
        console.error("Login error:", error.message);
      } else {
        console.error("Unexpected Error:", error);
      }
    }
  };

  const handleLogin = () => {
    loginToApp();
  };

  return (
    <div className={localStyles.mainContainer}>
      <img className={localStyles.logo} src={logo} alt="logo" />
      <div className={localStyles.loginDescription}>
        <p>
          Access your account securely with Microsoft 365 credentials. Simply
          provide your <br /> organization credentials to gain instant access to
          this portal
        </p>
      </div>

      <LoginButton onLogin={handleLogin} />
    </div>
  );
};

export default Login;
