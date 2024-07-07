import { useMsal } from "@azure/msal-react";
import { Outlet } from "react-router-dom";

const NavBar = () => {
  const { instance } = useMsal();

  const logout = () => {
    instance.logoutPopup({
      postLogoutRedirectUri: "/",
    });
  };

  return (
    <div className="w-screen h-screen flex flex-row space-x-5">
      <div className="w-1/6 flex flex-col">
        Nav Section
        <button onClick={logout}>Logout</button>
      </div>
      <div className="w-5/6">
        <Outlet />
      </div>
    </div>
  );
};

export default NavBar;
