import msIcon from "../../assets/icons/ms_icon.png";
import localStyles from "./Login.module.css";

type LoginButtonProps = {
  onLogin: () => void;
};

const LoginButton = (props: LoginButtonProps) => {
  return (
    <div className={localStyles.loginButton}>
      <button className="flex" onClick={props.onLogin}>
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
      </button>
    </div>
  );
};

export default LoginButton;
