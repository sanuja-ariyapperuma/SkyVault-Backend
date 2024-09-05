import {
  BrowserCacheLocation,
  Configuration,
  LogLevel,
} from "@azure/msal-browser";
import {
  authority,
  authRedirectURL,
  clientId,
} from "./features/Helpers/helper";

export const msalConfig: Configuration = {
  auth: {
    clientId: clientId,
    authority: authority,
    redirectUri: authRedirectURL,
  },
  cache: {
    cacheLocation: BrowserCacheLocation.LocalStorage,
    storeAuthStateInCookie: false,
  },
  system: {
    loggerOptions: {
      loggerCallback: (level, message, containsPii) => {
        if (containsPii) {
          return;
        }
        switch (level) {
          case LogLevel.Error:
            console.error(message);
            break;
          case LogLevel.Info:
            console.info(message);
            break;
          case LogLevel.Verbose:
            console.debug(message);
            break;
          case LogLevel.Warning:
            console.warn(message);
            break;
        }
      },
    },
  },
};

export const loginRequest = {
  scopes: ["api://e401d532-a867-4131-82c6-fe18242da055/readwrite"],
};
