import { Autocomplete, TextField } from "@mui/material";
import { OptionsType } from "../../features/Types/Dashboard/dashboardTypes";
import { useEffect, useState } from "react";

type CustomSelectProps = {
  options: OptionsType[];
  onChange: (selectedValue: string) => void;
  placeholder: string;
  initialValue?: string;
};

const CustomSelect = (props: CustomSelectProps) => {
  const { options, onChange, placeholder } = props;

  const [val, setVal] = useState<OptionsType | null>(null);

  useEffect(() => {
    if (props.initialValue) {
      const selectedOption = options.find(
        (option) => option.value === props.initialValue
      );
      if (selectedOption) {
        setVal(selectedOption);
      }
    }
  }, []);

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
      //onChange={handleChange}
      onChange={handleChange}
    />
  );
};

export default CustomSelect;
