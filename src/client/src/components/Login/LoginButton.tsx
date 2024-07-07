import msIcon from "../../assets/icons/ms_icon.png";
import localStyles from "./Login.module.css";

const LoginButton = () => {
  return (
    <div className={localStyles.loginButton}>
      <div className={localStyles.microsoftIconContainer}>
        <img
          src={msIcon}
          alt="Microsoft Icon"
          className={localStyles.iconInLoginButton}
        />
      </div>
      <div className={localStyles.loginText}>
        <span>Login with Microsoft 365</span>
      </div>
    </div>
  );
};

export default LoginButton;
