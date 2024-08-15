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
import { VISAType } from "../../features/Types/CustomerProfile/VISAType";

type VISAInfoProps = {
  country: OptionsType[];
  handleFieldChange: (field: string, value: any) => void;
  OnVISAEditClick: (editingVisa: VISAType) => void;
  onVISADeleteClick: (id: string) => void;
  initialVisa: VISAType;
};

const VISAInfo = (props: VISAInfoProps) => {
  const {
    country,
    handleFieldChange,
    OnVISAEditClick,
    initialVisa,
    onVISADeleteClick,
  } = props;

  const handleOnVISAExpiryChange = (newValue: Dayjs | null) => {
    handleFieldChange("expireDate", newValue?.format("DD/MM/YYYY"));
  };
  const handleOnVISAIssuedDateChange = (newValue: Dayjs | null) => {
    handleFieldChange("issuedDate", newValue?.format("DD/MM/YYYY"));
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
    handleFieldChange(
      "assignedToPrimaryPassport",
      !initialVisa.assignedToPrimaryPassport
    );
  };

  const handleOnVISAEditClick = (editingVisa: VISAType) => {
    OnVISAEditClick(editingVisa);
  };

  const handleOnVISADeleteClick = (id: string) => {
    onVISADeleteClick(id);
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
            value={initialVisa.visaNumber}
          />
          <input
            type="text"
            placeholder="VISA Issued Place"
            className={globalStyles.commonTextInput}
            onChange={handleOnVISAIssuedPlaceChange}
            value={initialVisa.issuedPlace}
          />
          <CustomDatePicker
            label="VISA Expiry Date"
            onDateChange={handleOnVISAExpiryChange}
            initialValue={initialVisa.expireDate}
          />
        </div>
        <div className={localStyles.accordionRight}>
          <CustomSelect
            options={country}
            onChange={handleOnCountrySelect}
            placeholder="Country"
            initialValue={initialVisa.countryId}
          />
          <CustomDatePicker
            label="VISA Issued Date"
            onDateChange={handleOnVISAIssuedDateChange}
            initialValue={initialVisa.issuedDate}
          />
          <FormControl>
            <Checkbox
              label="Assign with primary passport"
              checked={initialVisa.assignedToPrimaryPassport}
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
        <VISAList
          OnEditClick={handleOnVISAEditClick}
          OnDeleteClick={handleOnVISADeleteClick}
        />
      </div>
    </div>
  );
};

export default VISAInfo;
