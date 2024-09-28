import { Autocomplete, TextField } from "@mui/material";
import { OptionsType } from "../../features/Types/Dashboard/dashboardTypes";
import { useEffect, useState } from "react";

type CustomSelectProps = {
  options: OptionsType[];
  onChange: (selectedValue: string) => void;
  placeholder: string;
  initialValue?: string;
  isDisabled?: boolean;
};

const CustomSelect = (props: CustomSelectProps) => {
  const {
    options,
    onChange,
    placeholder,
    initialValue,
    isDisabled = false,
  } = props;

  const [val, setVal] = useState<OptionsType | null>(null);

  useEffect(() => {
    if (initialValue) {
      const selectedOption = options.find(
        (option) => option.value === initialValue
      );
      if (selectedOption) {
        setVal(selectedOption);
      } else {
        console.log("Selected Option not found");
      }
    } else {
      setVal(null);
    }
  }, [initialValue, options]);

  const handleChange = (
    _event: React.SyntheticEvent,
    value: OptionsType | null
  ) => {
    if (!value) {
      return;
    }
    setVal(value);
    onChange(value.value);
  };

  return (
    <Autocomplete
      disablePortal
      id="combo-box-demo"
      options={options}
      size="small"
      value={val}
      renderInput={(params) => (
        <TextField {...params} label={placeholder} variant="standard" />
      )}
      onChange={handleChange}
      disabled={isDisabled}
    />
  );
};

export default CustomSelect;
