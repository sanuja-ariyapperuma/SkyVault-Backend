import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import dayjs, { Dayjs } from "dayjs";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { useEffect, useState } from "react";

type CustomDatePickerProps = {
  label: string;
  onDateChange: (newValue: Dayjs | null) => void;
  initialValue?: Date | null;
};

const CustomDatePicker = (props: CustomDatePickerProps) => {
  const [val, setVal] = useState<Dayjs | null>(null);

  useEffect(() => {
    if (props.initialValue) {
      setVal(dayjs(props.initialValue));
    }
  });

  const handleOnDateChange = (newValue: Dayjs | null) => {
    setVal(newValue);
    props.onDateChange(newValue);
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="en-gb">
      <DatePicker
        label={props.label}
        sx={{
          "& .MuiOutlinedInput-root": {
            "& fieldset": {
              border: "none",
              borderBottom: 2,
              borderBottomColor: "#DBE1E5",
            },
          },
        }}
        slotProps={{
          textField: {
            size: "small",
          },
        }}
        onChange={handleOnDateChange}
        value={val}
      />
    </LocalizationProvider>
  );
};

export default CustomDatePicker;
