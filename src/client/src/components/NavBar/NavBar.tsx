import { useMsal } from "@azure/msal-react";
import { Outlet, useNavigate } from "react-router-dom";

import localStyles from "./NavBar.module.css";
import dashboardLogo from "../../assets/dashboard_logo.png";
import defaultUserImage from "../../assets/default_user.jpg";
import logoutIcon from "../../assets/icons/logout.webp";
import NavItem from "./NavItem";
import { ToastContainer } from "react-toastify";
import { useCookies } from "react-cookie";
import { useEffect, useState } from "react";

import profileNavIcon from "../../assets/icons/pm_active.png";
import notificationIcon from "../../assets/icons/notification.png";
import transferProfileIcon from "../../assets/icons/switch-1.png";

const NavBar = () => {
  const { instance, accounts } = useMsal();

  const [cookie, _, removeCookie] = useCookies(["TravelChannel"]);
  const [fullName, setFullName] = useState<string>("");
  const [userRole, setUserRole] = useState<string>("");

  const navigate = useNavigate();

  const logout = () => {
    const logoutRequest = {
      account: accounts[0],
      postLogoutRedirectUri: "/",
    };
    instance.logoutRedirect(logoutRequest).then(() => {
      removeCookie("TravelChannel");
    });
  };

  useEffect(() => {
    const activeAccount = accounts[0];
    setFullName(activeAccount.name ?? "");
    setUserRole(
      activeAccount.idTokenClaims?.roles?.length ?? 0 > 0
        ? activeAccount.idTokenClaims?.roles?.[0] ?? ""
        : ""
    );
  }, [cookie]);

  return (
    <div className={localStyles.mainContainer}>
      <div className={localStyles.navContainer}>
        <div className={localStyles.headerContainer}>
          {/* Header Area */}
          <div
            className={localStyles.brandContainer}
            onClick={() => navigate("/")}
            role="button"
            style={{ cursor: "pointer" }}
          >
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
          <NavItem
            iconPath={profileNavIcon}
            navName={"Profile Management"}
            isActive={true}
            path="/customer-profile"
          />
          <NavItem
            iconPath={notificationIcon}
            navName={"Notifications"}
            isActive={false}
            path=""
          />
          <NavItem
            iconPath={transferProfileIcon}
            navName={"Transfer Profiles"}
            isActive={false}
            path=""
          />
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
              <span className={localStyles.brandText}>{fullName}</span>
              <span className={localStyles.versionText}>{userRole}</span>
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
      <ToastContainer />
    </div>
  );
};

export default NavBar;
