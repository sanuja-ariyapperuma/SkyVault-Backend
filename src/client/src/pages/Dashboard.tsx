import { useState } from "react";
import Select, { SingleValue } from "react-select";
import axios from "axios";

import localStyles from "../components/Dashboard/Dashboard.module.css";
import NavItem from "../components/NavBar/NavItem";
import { useMsal } from "@azure/msal-react";
import { debounce } from "lodash";

import {
  OptionsType,
  SearchResponse,
} from "../features/Types/Dashboard/dashboardTypes";
import { useNavigate } from "react-router-dom";
import { baseURL } from "../features/Helpers/helper";

const Dashboard = () => {
  const { instance, accounts } = useMsal();
  const [options, setOptions] = useState<OptionsType[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const navigate = useNavigate();

  const request = {
    scopes: [import.meta.env.VITE_AD_SCOPE as string],
    account: accounts[0],
  };

  const handleSearch = debounce((newValue: string) => {
    setIsLoading(true);
    const body = {
      SysUserId: "9",
      SearchQuery: newValue,
    };

    if (newValue.length == 0) {
      setOptions([]);
      setIsLoading(false);
      return;
    }

    instance.acquireTokenSilent(request).then(async (response) => {
      const accessToken = response.accessToken;

      try {
        const response_1 = await axios.post<SearchResponse>(
          `${baseURL}/searchprofile`,
          body,
          {
            headers: {
              Authorization: `Bearer ${accessToken}`,
              ContentType: "application/json",
            },
          }
        );
        const options = response_1.data.profiles.map((profile): OptionsType => {
          return {
            value: profile.profileId,
            label: `${profile.salutation} ${profile.lastName} ${profile.otherNames} | ${profile.passportNumber}`,
          };
        });
        setOptions(options);
      } catch (err) {
        return console.error(err);
      } finally {
        setIsLoading(false);
      }
    });
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
        />
      </div>
    </div>
  );
};

export default Dashboard;
