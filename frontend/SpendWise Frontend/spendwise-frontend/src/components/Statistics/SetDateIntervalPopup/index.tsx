import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Divider,
} from "@mui/material";
import { FC, useState } from "react";

import "react-datepicker/dist/react-datepicker.css";
import "./SetDateIntervalPopup.css";
import { DateSelector } from "../DateSelector";

interface SetDateIntervalPopupProps {
  open: boolean;
  onClose: () => void;
  onEditing: (startTime: Date | null, endTime: Date | null) => void;
}

export const SetDateIntervalPopup: FC<SetDateIntervalPopupProps> = ({
  open,
  onClose,
  onEditing,
}: SetDateIntervalPopupProps) => {
  const [startDate, setStartDate] = useState<Date | null>(null);
  const [endDate, setEndDate] = useState<Date | null>(null);

  const handleClose = () => {
    setStartDate(null);
    setEndDate(null);
    onClose();
  };

  const handleSave = () => {
    onEditing(startDate, endDate);
    handleClose();
  };

  return (
    <Dialog fullWidth={true} open={open} onClose={onClose}>
      <DialogTitle color={"primary"}>Set Date Interval</DialogTitle>

      <Divider/>

      <DialogContent className={"dialog-content"}>
        <Box className={"date-picker-container"}>
          <DateSelector
            dateType="Start"
            onEditing={(date: Date) => {
              setStartDate(date);
            }}
          />
          <DateSelector
            dateType="End"
            onEditing={(date: Date) => {
              setEndDate(date);
            }}
          />
        </Box>
      </DialogContent>

      <DialogActions>
        <Button onClick={handleClose} variant="outlined">
          Close
        </Button>
        <Button
          onClick={handleSave}
          variant="contained"
          disabled={!startDate || !endDate}
          className="save-button"
        >
          Save
        </Button>
      </DialogActions>
    </Dialog>
  );
};
