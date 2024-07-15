import localStyles from "../components/Dashboard/Dashboard.module.css";

import Select from "react-select";
import NavItem from "../components/NavBar/NavItem";

const Dashboard = () => {
  //const tokenReducer = useSelector((state: RootState) => state.tokenR);

  const options = [
    { value: "chocolate", label: "Chocolate" },
    { value: "strawberry", label: "Strawberry" },
    { value: "vanilla", label: "Vanilla" },
  ];

  return (
    <div className={localStyles.myClass}>
      <div className={localStyles.buttonArea}>
        <NavItem iconPath="" navName="Create Profile" />
      </div>
      <div className={localStyles.searchArea}>
        <Select options={options} />
      </div>
    </div>
  );
};

export default Dashboard;
