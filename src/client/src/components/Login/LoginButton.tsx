import msIcon from "../../assets/icons/ms_icon.png";
import localStyles from "./Login.module.css";

type LoginButtonProps = {
  onLogin: () => void;
  isDisabled: boolean;
};

const LoginButton = (props: LoginButtonProps) => {
  return (
    <div
      className={
        props.isDisabled
          ? localStyles.loginButtonDisabled
          : localStyles.loginButton
      }
    >
      <button
        className="flex"
        onClick={props.onLogin}
        disabled={props.isDisabled}
      >
        <div className={localStyles.microsoftIconContainer}>
          {!props.isDisabled && (
            <img
              src={msIcon}
              alt="Microsoft Icon"
              className={localStyles.iconInLoginButton}
            />
          )}
        </div>
        <div className={localStyles.loginText}>
          <span>Login with Microsoft 365</span>
        </div>
      </button>
    </div>
  );
};

export default LoginButton;
