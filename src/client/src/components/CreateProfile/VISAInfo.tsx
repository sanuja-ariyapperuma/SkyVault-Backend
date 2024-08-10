import FormControl from "@mui/joy/FormControl";
import FormHelperText from "@mui/joy/FormHelperText";
import { Checkbox } from "@mui/joy";
import { Dayjs } from "dayjs";

import localStyles from "./CustomerProfile.module.css";
import globalStyles from "../CommonComponents/Common.module.css";
import CustomDatePicker from "../CommonComponents/CustomDatePicker";
import { OptionsType } from "../../features/Types/Dashboard/dashboardTypes";
import VISAList from "./VISAList";
import CustomSelect from "../CommonComponents/CustomSelect";
import { ChangeEvent, useState } from "react";

type VISAInfoProps = {
  country: OptionsType[];
  handleFieldChange: (field: string, value: any) => void;
};

const VISAInfo = (props: VISAInfoProps) => {
  const { country, handleFieldChange } = props;
  const [assignedToPrimaryPassport, setAssignedToPrimaryPassport] =
    useState<boolean>(true);

  const handleOnVISAExpiryChange = (newValue: Dayjs | null) => {
    handleFieldChange("expiryDate", newValue);
  };
  const handleOnVISAIssuedDateChange = (newValue: Dayjs | null) => {
    handleFieldChange("issuedDate", newValue);
  };
  const handleOnCountrySelect = (value: string) => {
    handleFieldChange("countryId", value);
  };
  const handleOnVISANumberChange = (event: ChangeEvent<HTMLInputElement>) => {
    handleFieldChange("visaNumber", event.target.value);
  };
  const handleOnVISAIssuedPlaceChange = (
    event: ChangeEvent<HTMLInputElement>
  ) => {
    handleFieldChange("issuedPlace", event.target.value);
  };
  const handleOnAssignChange = (event: ChangeEvent<HTMLInputElement>) => {
    handleFieldChange("assignedToPrimaryPassport", assignedToPrimaryPassport);
    setAssignedToPrimaryPassport(!assignedToPrimaryPassport);
  };

  return (
    <div className={localStyles.visaContainer}>
      <div className={localStyles.accordionContent}>
        <div className={localStyles.accordionLeft}>
          <input
            type="text"
            placeholder="VISA Number"
            className={globalStyles.commonTextInput}
            onChange={handleOnVISANumberChange}
          />
          <input
            type="text"
            placeholder="VISA Issued Place"
            className={globalStyles.commonTextInput}
            onChange={handleOnVISAIssuedPlaceChange}
          />
          <CustomDatePicker
            label="VISA Expiry Date"
            onDateChange={handleOnVISAExpiryChange}
          />
        </div>
        <div className={localStyles.accordionRight}>
          <CustomSelect
            options={country}
            onChange={handleOnCountrySelect}
            placeholder="Country"
          />
          <CustomDatePicker
            label="VISA Issued Date"
            onDateChange={handleOnVISAIssuedDateChange}
          />
          <FormControl>
            <Checkbox
              label="Assign with primary passport"
              checked={assignedToPrimaryPassport}
              onChange={handleOnAssignChange}
            />
            <FormHelperText>
              Uncheck to assign to secondary passport.
            </FormHelperText>
          </FormControl>
        </div>
      </div>
      <br />
      <div className={localStyles.VISAListArea}>
        <VISAList />
      </div>
    </div>
  );
};

export default VISAInfo;
