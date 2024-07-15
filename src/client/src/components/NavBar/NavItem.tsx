import localStyles from "./NavItem.module.css";
import pm_active from "../../assets/icons/pm_active.png";

type NavItemPropsType = {
  iconPath: string;
  navName: string;
};

const NavItem = (props: NavItemPropsType) => {
  return (
    <div className={localStyles.navItem}>
      <img
        src={pm_active}
        alt={props.navName}
        className={localStyles.navIcon}
      />
      <span>{props.navName}</span>
    </div>
  );
};

export default NavItem;
