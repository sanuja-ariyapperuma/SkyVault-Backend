import { OptionsType } from "../../features/Types/Dashboard/dashboardTypes";
import CustomDatePicker from "../CommonComponents/CustomDatePicker";
import localStyles from "../CreateProfile/CustomerProfile.module.css";

import globalStyles from "../CommonComponents/Common.module.css";
import CustomSelect from "../CommonComponents/CustomSelect";
import { ChangeEvent } from "react";
import { PassportType } from "../../features/Types/CustomerProfile/PassportType";
import { Typography } from "@mui/material";

type PassportInfoProps = {
  salutations: OptionsType[];
  gender: OptionsType[];
  nationality: OptionsType[];
  country: OptionsType[];
  customerPassport: PassportType;
  handleFieldChange: (field: string, value: any) => void;
  isSecondary?: boolean;
};

const PassportInfo = (props: PassportInfoProps) => {
  const {
    salutations,
    gender,
    nationality,
    country,
    customerPassport,
    handleFieldChange,
    isSecondary = false,
  } = props;

  const handleSalutationSelect = (value: string) => {
    handleFieldChange("salutationId", value);
  };
  const handleGenderSelect = (value: string) => {
    handleFieldChange("genderId", value);
  };
  const handleNationalityOnSelect = (value: string) => {
    handleFieldChange("nationalityId", value);
  };
  const handleOnCountrySelect = (value: string) => {
    handleFieldChange("countryId", value);
  };
  const handleOnDateOfBirthChange = (newValue: string) => {
    handleFieldChange("dateOfBirth", newValue);
  };
  const handleOnPassportExpiryChange = (newValue: string) => {
    handleFieldChange("passportExpiryDate", newValue);
  };
  const handleOnPassportNumberChange = (
    event: ChangeEvent<HTMLInputElement>
  ) => {
    handleFieldChange("passportNumber", event.target.value);
  };
  const handleOnPleaceOfBirthChange = (
    event: ChangeEvent<HTMLInputElement>
  ) => {
    handleFieldChange("placeOfBirth", event.target.value);
  };
  const handleLastNameChange = (event: ChangeEvent<HTMLInputElement>) => {
    handleFieldChange("lastName", event.target.value);
  };
  const handleOtherNameChange = (event: ChangeEvent<HTMLInputElement>) => {
    handleFieldChange("otherNames", event.target.value);
  };

  return (
    <div>
      <div>
        <Typography variant="h6" style={{ marginLeft: "5px" }}>
          {" "}
          {customerPassport.passportCode}
        </Typography>
      </div>
      <div className={localStyles.accordionContent}>
        <div className={localStyles.accordionLeft}>
          <CustomSelect
            options={salutations}
            onChange={handleSalutationSelect}
            placeholder="Salutation"
            initialValue={customerPassport.salutationId}
            isDisabled={isSecondary}
          />
          <input
            type="text"
            placeholder="Other names"
            className={globalStyles.commonTextInput}
            value={customerPassport.otherNames}
            onChange={handleOtherNameChange}
            disabled={isSecondary}
          />
          <CustomSelect
            options={gender}
            onChange={handleGenderSelect}
            placeholder="Gender"
            initialValue={customerPassport.genderId}
            isDisabled={isSecondary}
          />
          <input
            type="text"
            placeholder="Passport Number"
            className={globalStyles.commonTextInput}
            value={customerPassport.passportNumber}
            onChange={handleOnPassportNumberChange}
          />
          <CustomDatePicker
            label="Passport Expiry Date"
            onDateChange={handleOnPassportExpiryChange}
            initialValue={customerPassport.passportExpiryDate}
          />
        </div>
        <div className={localStyles.accordionRight}>
          <input
            type="text"
            placeholder="Last Name"
            className={globalStyles.commonTextInput}
            value={customerPassport.lastName}
            onChange={handleLastNameChange}
            disabled={isSecondary}
          />
          <CustomSelect
            options={nationality}
            onChange={handleNationalityOnSelect}
            placeholder="Nationality"
            initialValue={customerPassport.nationalityId}
          />
          <input
            type="text"
            placeholder="Place of Birth"
            className={globalStyles.commonTextInput}
            value={customerPassport.placeOfBirth}
            onChange={handleOnPleaceOfBirthChange}
            disabled={isSecondary}
          />
          <CustomDatePicker
            label="Date of Birth"
            onDateChange={handleOnDateOfBirthChange}
            initialValue={customerPassport.dateOfBirth}
            disableFuture={true}
            isDisabled={isSecondary}
          />
          <CustomSelect
            options={country}
            onChange={handleOnCountrySelect}
            placeholder="Country"
            initialValue={customerPassport.countryId}
          />
        </div>
      </div>
    </div>
  );
};

export default PassportInfo;
