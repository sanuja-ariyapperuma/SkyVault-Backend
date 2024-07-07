import { useMsal } from "@azure/msal-react";
import { Outlet } from "react-router-dom";

import localStyles from "./NavBar.module.css";
import dashboardLogo from "../../assets/dashboard_logo.png";
import defaultUserImage from "../../assets/default_user.jpg";
import logoutIcon from "../../assets/icons/logout.webp";

const NavBar = () => {
  const { instance } = useMsal();

  const logout = () => {
    instance.logoutPopup({
      postLogoutRedirectUri: "/",
    });
  };

  return (
    <div className={localStyles.mainContainer}>
      <div className={localStyles.navContainer}>
        <div className={localStyles.headerContainer}>
          {/* Header Area */}
          <div className={localStyles.brandContainer}>
            <img
              className={localStyles.brandLogo}
              src={dashboardLogo}
              alt="logo"
            />
            <div className={localStyles.brandDetails}>
              <span className={localStyles.brandText}>Travel Manager</span>
              <span className={localStyles.versionText}>v1.0.0.0</span>
            </div>
          </div>
        </div>
        <div className={localStyles.menuContainer}>
          {/* Menu Area */}
          Menus
        </div>
        <div className={localStyles.footerContainer}>
          {/* Footer Area */}
          <div className={localStyles.userInformation}>
            <img
              className={localStyles.userDisplayPicture}
              src={defaultUserImage}
              alt="user"
            />
            <div className={localStyles.userInfo}>
              <span className={localStyles.brandText}>John Doe</span>
              <span className={localStyles.versionText}>Administrator</span>
            </div>
          </div>
          <div className={localStyles.logoutButtonArea}>
            <button className={localStyles.logoutButton} onClick={logout}>
              <img className={localStyles.logoutIcon} src={logoutIcon}></img>
              <span>Logout</span>
            </button>
          </div>
        </div>
      </div>
      <div className={localStyles.pagesContainer}>
        <Outlet />
      </div>
    </div>
  );
};

export default NavBar;
