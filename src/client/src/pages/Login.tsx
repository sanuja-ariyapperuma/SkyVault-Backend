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
import { useDispatch } from "react-redux";
import { AppDispatch } from "../store";
import { setAccessToken } from "../features/reducers/tokenSlice";
import axios from "axios";
import { baseURL } from "../features/services/apiCalls";

const Login = () => {
  const { instance, accounts } = useMsal();
  const navigate = useNavigate();

  const dispatch: AppDispatch = useDispatch();

  useEffect(() => {
    if (accounts.length > 0) {
      navigate("/");
    }
  });

  const handleAPIAuthentication = (upn: string, accessToken: string) => {
    const body = {
      upn: upn,
    };

    axios
      .post(`${baseURL}/auth/user`, body, {
        headers: {
          Authorization: `Bearer ${accessToken}`,
          ContentType: "application/json",
        },
      })
      .then((res) => console.log(res.data))
      .catch((err) => console.error(err));
  };

  const loginToApp = async () => {
    await instance
      .loginPopup({
        //scopes: ["user.read"],
        scopes: ["api://e401d532-a867-4131-82c6-fe18242da055/access_as_user"],
        account: accounts[0],
      })
      .then((response) => {
        dispatch(setAccessToken(response.accessToken));
        console.log("Access token acquired: ", response.accessToken);
        handleAPIAuthentication(
          response.account.username,
          response.accessToken
        );
        // getUser({
        //   upn: response.account.username,
        // });
      })
      .catch((error) => {
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
