import localStyles from "../components/Login/Login.module.css";
import logo from "../assets/login_logo.png";
import LoginButton from "../components/Login/LoginButton";
import { useMsal } from "@azure/msal-react";
import {
  AuthError,
  BrowserAuthError,
  InteractionRequiredAuthError,
} from "@azure/msal-browser";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { baseURL, scopes } from "../features/Helpers/helper";
import { AuthenticatedResponse } from "../features/Types/Login/authenticatedResponse";
import { notifyError } from "../components/CommonComponents/Toasters";

const Login = () => {
  const { instance, accounts } = useMsal();
  const navigate = useNavigate();
  const adScopes = scopes;

  const [isHealthy, setIsHealthy] = useState(false);

  useEffect(() => {
    axios
      .get(`${baseURL}/health`)
      .then((response) => {
        if (response.status === 200) {
          setIsHealthy(true);
          if (accounts.length > 0) {
            navigate("/");
          }
        } else {
          notifyError(
            "Something went wrong. Please try again later or contact system administrator"
          );
          console.error("API is not healthy");
        }
      })
      .catch((e) => {
        console.error("API is not healthy", e);
        notifyError(
          "Something went wrong. Please try again later or contact system administrator"
        );
      });
  }, [accounts]);

  const handleAPIAuthentication = (
    upn: string,
    userRole: string,
    accessToken: string
  ) => {
    const body = {
      upn: upn,
      userRole: userRole,
    };

    axios
      .post<AuthenticatedResponse>(`${baseURL}/auth/user`, body, {
        headers: {
          Authorization: `Bearer ${accessToken}`,
          ContentType: "application/json",
        },
        withCredentials: true,
      })
      .then((res) => {
        if (res.status !== 200) {
          notifyError("Failed to authenticate user");
          navigate("/login");
          return;
        }
      })
      .catch(() => {
        notifyError("Failed to authenticate user");
      });
  };

  const loginToApp = async () => {
    await instance
      .loginPopup({
        scopes: [adScopes],
        account: accounts[0],
      })
      .then((response) => {
        const upn = (response.idTokenClaims as any).preferred_username;
        const userRole = (response.idTokenClaims as any).roles[0];
        const accessToken = response.accessToken;

        handleAPIAuthentication(upn, userRole, accessToken);
      })
      .catch((error) => {
        notifyError("System Error: Failed to login");
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
      });
  };

  const handleLogin = (): void => {
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

      <LoginButton onLogin={handleLogin} isDisabled={!isHealthy} />
    </div>
  );
};

export default Login;
