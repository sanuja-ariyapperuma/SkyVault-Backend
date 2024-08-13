import ReactDOM from "react-dom/client";
import "./index.css";
import { RouterProvider } from "react-router-dom";

import { router } from "./routes/routes.tsx";
import { StrictMode } from "react";
import { MsalProvider } from "@azure/msal-react";
import { msalConfig } from "./authConfig.ts";
import { PublicClientApplication } from "@azure/msal-browser";
import { Provider } from "react-redux";
import { store } from "./store.ts";
import "react-toastify/ReactToastify.css";
import TestComponent from "./components/CommonComponents/TestComponent.tsx";

const msalInstance = new PublicClientApplication(msalConfig);

ReactDOM.createRoot(document.getElementById("root")!).render(
  <Provider store={store}>
    <MsalProvider instance={msalInstance}>
      <RouterProvider router={router} />
    </MsalProvider>
  </Provider>
);
