import { Box, Input, Typography } from "@mui/material";
import { FC, useState } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { FaCalendarAlt } from "react-icons/fa";

import "./DateSelector.css";

interface DateSelectorProps {
  dateType: string;
  onEditing: (date: Date) => void;
}

export const DateSelector: FC<DateSelectorProps> = ({
  dateType,
  onEditing,
}: DateSelectorProps) => {
  const [date, setDate] = useState<Date | null>(null);
  const handleChange = (date: Date) => {
    setDate(date);
    onEditing(date);
  };

  return (
    <Box className={"date-selector-container"}>
      <Typography color={"primary"}>{dateType}</Typography>
      <DatePicker
        selected={date}
        onChange={handleChange}
        className={"date-picker"}
        customInput={
            <Box className="date-input">
              <Input
                className="date-input-field"
                value={date ? date.toLocaleDateString() : ""}
                readOnly
              />
              <FaCalendarAlt className="calendar-icon" />
            </Box>
          }
      />
    </Box>
  );
};
