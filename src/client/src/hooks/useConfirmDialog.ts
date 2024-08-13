import { useState } from "react";

type ConfirmDialogState = {
  open: boolean;
  content?: string;
  onClose?: () => void;
  onConfirm?: () => void;
};

export const useConfirmDialog = () => {
  const [dialogState, setDialogState] = useState<ConfirmDialogState>({
    open: false,
  });

  const openDialog = (message: string, onConfirm: () => void) => {
    setDialogState({ open: true, content: message, onConfirm });
  };

  const closeDialog = () => {
    setDialogState({
      ...dialogState,
      open: false,
    });
  };

  const confirmDialog = () => {
    if (dialogState.onConfirm) {
      dialogState.onConfirm();
    } else {
      console.error("onConfirm is not defined");
    }

    closeDialog();
  };

  return {
    openDialog,
    closeDialog,
    dialogProps: {
      ...dialogState,
      onConfirm: confirmDialog,
      onClose: closeDialog,
    },
  };
};
