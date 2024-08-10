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

type VISAInfoProps = {
  country: OptionsType[];
  handleOnDateOfBirthChange: (newValue: Dayjs | null) => void;
  handleOnCountrySelect: (value: string) => void;
};

const VISAInfo = (props: VISAInfoProps) => {
  const { country, handleOnDateOfBirthChange, handleOnCountrySelect } = props;
  return (
    <div className={localStyles.visaContainer}>
      <div className={localStyles.accordionContent}>
        <div className={localStyles.accordionLeft}>
          <input
            type="text"
            placeholder="VISA Number"
            className={globalStyles.commonTextInput}
          />
          <input
            type="text"
            placeholder="VISA Issued Place"
            className={globalStyles.commonTextInput}
          />
          <CustomDatePicker
            label="VISA Expiry Date"
            handleOnDateChange={handleOnDateOfBirthChange}
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
            handleOnDateChange={handleOnDateOfBirthChange}
          />
          <FormControl>
            <Checkbox label="Assign with primary passport" />
            <FormHelperText>
              Uncheck to assign to secondary passport.
            </FormHelperText>
          </FormControl>
        </div>
      </div>
      <div className={localStyles.VISAListArea}>
        <VISAList />
      </div>
    </div>
  );
};

export default VISAInfo;
