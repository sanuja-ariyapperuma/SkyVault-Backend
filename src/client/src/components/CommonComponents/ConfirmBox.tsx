import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import { useState } from "react";

type ConfirmDialogProps = {
  open: boolean;
  content?: string;
  onClose: () => void;
  onConfirm: () => void;
};

const ConfirmBox = (props: ConfirmDialogProps) => {
  const { open, content, onClose, onConfirm } = props;

  const handleConfirmClick = () => {
    onConfirm();
  };

  const handleDisagreeClick = () => {
    onClose();
  };

  return (
    <Dialog
      open={open}
      onClose={handleDisagreeClick}
      aria-labelledby="alert-dialog-title"
      aria-describedby="alert-dialog-description"
    >
      {/* <DialogTitle id="alert-dialog-title">
        {"Use Google's location service?"}
      </DialogTitle> */}
      <DialogContent>
        <DialogContentText id="alert-dialog-description">
          {content}
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleDisagreeClick} autoFocus>
          Close
        </Button>
        <Button onClick={handleConfirmClick}>Confirm</Button>
      </DialogActions>
    </Dialog>
  );
};

export default ConfirmBox;
