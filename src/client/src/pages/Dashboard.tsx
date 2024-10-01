import { useState } from "react";
import Select, { SingleValue } from "react-select";

import localStyles from "../components/Dashboard/Dashboard.module.css";
import NavItem from "../components/NavBar/NavItem";
import { debounce } from "lodash";

import { OptionsType } from "../features/Types/Dashboard/dashboardTypes";
import { useNavigate } from "react-router-dom";
import { notifyError } from "../components/CommonComponents/Toasters";
import { searchProfiles } from "../features/services/Dashboard.ts/apiMethods";

const Dashboard = () => {
  const [options, setOptions] = useState<OptionsType[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const navigate = useNavigate();

  const handleSearch = debounce(async (newValue: string) => {
    setOptions([]);
    setIsLoading(true);

    if (newValue.length == 0) {
      setOptions([]);
      setIsLoading(false);
      return;
    }

    try {
      const searchResponse = await searchProfiles(newValue);
      const profiles = searchResponse.profiles;

      if (profiles.length === 0) {
        const option: OptionsType = {
          value: "",
          label: "No profile found",
        };
        setOptions([option]);
      } else {
        const optionsFromDb = profiles.map((profile): OptionsType => {
          return {
            value: profile.profileId,
            label: `${profile.salutation} ${profile.lastName} ${profile.otherNames} | ${profile.passportNumber}`,
          };
        });
        setOptions(optionsFromDb);
      }
    } catch (err) {
      notifyError("Something went wrong. Search is failing");
      return console.error(err);
    } finally {
      setIsLoading(false);
    }
  }, 500);

  const handleSelect = (newValue: SingleValue<OptionsType>) => {
    navigate(`customer-profile/${newValue?.value}`);
  };

  return (
    <div className={localStyles.myClass}>
      <div className={localStyles.buttonArea}>
        <NavItem iconPath="" navName="Create Profile" />
      </div>
      <div className={localStyles.searchArea}>
        <Select
          options={options}
          onInputChange={handleSearch}
          isLoading={isLoading}
          onChange={handleSelect}
          noOptionsMessage={() => "No Profiles Found"}
        />
      </div>
    </div>
  );
};

export default Dashboard;
