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

  const { initialValue, onDateChange, label } = props;

  dayjs.locale("en-gb");

  useEffect(() => {
    if (initialValue) {
      setVal(dayjs(initialValue));
    } else {
      setVal(null);
    }
  }, [initialValue]);

  const handleOnDateChange = (newValue: Dayjs | null) => {
    setVal(newValue);
    onDateChange(newValue);
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale={"en-gb"}>
      <DatePicker
        format="DD/MM/YYYY"
        label={label}
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
