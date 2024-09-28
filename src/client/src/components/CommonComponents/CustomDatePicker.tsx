import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import dayjs, { Dayjs } from "dayjs";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { useEffect, useState } from "react";
import { notifyError } from "./Toasters";
import "dayjs/locale/en-gb";

type CustomDatePickerProps = {
  label: string;
  onDateChange: (newValue: string) => void;
  initialValue?: string;
  disableFuture?: boolean;
  isDisabled?: boolean;
};

const CustomDatePicker = (props: CustomDatePickerProps) => {
  const [val, setVal] = useState<Dayjs | null>(null);

  const {
    initialValue,
    onDateChange,
    label,
    disableFuture = false,
    isDisabled = false,
  } = props;

  useEffect(() => {
    if (initialValue) {
      const parsedDate = dayjs(initialValue, "DD/MM/YYYY", true);
      if (parsedDate.isValid()) {
        setVal(parsedDate);
      } else {
        notifyError("Invalid initial date format");
        setVal(null);
      }
    } else {
      setVal(null);
    }
  }, [initialValue]);

  const handleOnDateChange = (newValue: Dayjs | null) => {
    if (newValue && newValue.isValid()) {
      const formattedDate = newValue.format("DD/MM/YYYY");
      setVal(newValue);
      onDateChange(formattedDate);
    } else {
      setVal(null);
    }
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
        disableFuture={disableFuture}
        disabled={isDisabled}
      />
    </LocalizationProvider>
  );
};

export default CustomDatePicker;
