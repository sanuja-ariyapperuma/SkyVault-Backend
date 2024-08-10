import { Bounce, toast, ToastOptions } from "react-toastify";

const toasterConfig: ToastOptions = {
  position: "top-right",
  autoClose: 5000,
  hideProgressBar: false,
  closeOnClick: true,
  pauseOnHover: true,
  draggable: true,
  progress: undefined,
  theme: "light",
  transition: Bounce,
};

export const notifyError = (message: string) =>
  toast.error(message, toasterConfig);

export const notifySuccess = (message: string) =>
  toast.success(message, toasterConfig);
