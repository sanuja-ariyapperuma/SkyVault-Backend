import localStyles from "../components/Login/Login.module.css";
import logo from "../assets/login_logo.png";
import LoginButton from "../components/Login/LoginButton";

const Login = () => {
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

      <LoginButton />
    </div>
  );
};

export default Login;
