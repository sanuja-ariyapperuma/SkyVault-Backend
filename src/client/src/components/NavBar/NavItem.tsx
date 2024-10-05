import localStyles from "./NavItem.module.css";
import { useNavigate } from "react-router-dom";

type NavItemPropsType = {
  iconPath: string;
  navName: string;
  isActive: boolean;
  path: string;
};

const NavItem = (props: NavItemPropsType) => {
  const navigate = useNavigate();

  const handleClick = () => navigate(props.path);

  return (
    <button
      className={
        props.isActive ? localStyles.navItem : localStyles.navItemInactive
      }
      onClick={handleClick}
    >
      <img
        src={props.iconPath}
        alt={props.navName}
        className={localStyles.navIcon}
      />
      <span>{props.navName}</span>
    </button>
  );
};

export default NavItem;
