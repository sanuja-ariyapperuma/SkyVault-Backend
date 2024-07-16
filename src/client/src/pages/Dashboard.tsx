import { useState } from "react";
import Select, { SingleValue } from "react-select";
import axios from "axios";

import localStyles from "../components/Dashboard/Dashboard.module.css";
import NavItem from "../components/NavBar/NavItem";
import { baseURL } from "../features/services/apiCalls";
import { useMsal } from "@azure/msal-react";
import { debounce } from "lodash";

import {
  OptionsType,
  SearchResponse,
} from "../features/Types/Dashboard/dashboardTypes";
import { useNavigate } from "react-router-dom";

const Dashboard = () => {
  const { instance, accounts } = useMsal();
  const [options, setOptions] = useState<OptionsType[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const navigate = useNavigate();

  const request = {
    scopes: ["api://e401d532-a867-4131-82c6-fe18242da055/access_as_user"],
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
          const options = response_1.data.profiles.map(
            (profile): OptionsType => {
              return {
                value: profile.profileId,
                label: `${profile.salutation} ${profile.lastName} ${profile.otherNames} | ${profile.passportNumber}`,
              };
            }
          );
          setOptions(options);
        } catch (err) {
          return console.error(err);
        }
      } finally {
        setIsLoading(false);
      }
    });
  }, 500);

  const handleSelect = (newValue: SingleValue<OptionsType>) => {
    console.log(newValue);
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
