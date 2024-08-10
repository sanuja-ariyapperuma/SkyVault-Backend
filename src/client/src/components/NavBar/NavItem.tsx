import localStyles from "./NavItem.module.css";
import pm_active from "../../assets/icons/pm_active.png";
import { useNavigate } from "react-router-dom";

type NavItemPropsType = {
  iconPath: string;
  navName: string;
};

const NavItem = (props: NavItemPropsType) => {
  const navigate = useNavigate();

  const handleClick = () => navigate("/customer-profile");

  return (
    <button className={localStyles.navItem} onClick={handleClick}>
      <img
        src={pm_active}
        alt={props.navName}
        className={localStyles.navIcon}
      />
      <span>{props.navName}</span>
    </button>
  );
};

export default NavItem;
