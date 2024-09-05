import ReactDOM from "react-dom/client";
import "./index.css";
import { RouterProvider } from "react-router-dom";

import { router } from "./routes/routes.tsx";
import { MsalProvider } from "@azure/msal-react";
import { msalConfig } from "./authConfig.ts";
import {
  AuthenticationResult,
  EventType,
  PublicClientApplication,
} from "@azure/msal-browser";
import { Provider } from "react-redux";
import { store } from "./store.ts";
import "react-toastify/ReactToastify.css";

export const msalInstance = new PublicClientApplication(msalConfig);

const isAuthenticationResult = (
  payload: any
): payload is AuthenticationResult => {
  return (payload as AuthenticationResult).account !== undefined;
};

if (
  !msalInstance.getActiveAccount() &&
  msalInstance.getAllAccounts().length > 0
) {
  msalInstance.setActiveAccount(msalInstance.getAllAccounts()[0]);
}

//Listen the sign in event and set the active account
msalInstance.addEventCallback((event) => {
  if (
    event.eventType === EventType.LOGIN_SUCCESS &&
    isAuthenticationResult(event.payload)
  ) {
    const account = event.payload.account;
    msalInstance.setActiveAccount(account);
  }
});

ReactDOM.createRoot(document.getElementById("root")!).render(
  <Provider store={store}>
    <MsalProvider instance={msalInstance}>
      <RouterProvider router={router} />
    </MsalProvider>
  </Provider>
);
