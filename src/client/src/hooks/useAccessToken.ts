import { useMsal } from "@azure/msal-react";
import { useState, useEffect } from "react";
import { SilentRequest } from "@azure/msal-browser";
import { scopes } from "../features/Helpers/helper";

export const useAccessToken = () => {
  const { instance, accounts } = useMsal();
  const [accessToken, setAccessToken] = useState<string | null>(null);

  useEffect(() => {
    const fetchAccessToken = async () => {
      try {
        if (!instance.getAllAccounts().length) {
          console.error("MSAL instance is not initialized.");
          return;
        }

        const request: SilentRequest = {
          scopes: [scopes],
          account: accounts[0],
        };

        const response = await instance.acquireTokenSilent(request);
        setAccessToken(response.accessToken);
      } catch (error) {
        console.error("Failed to acquire access token:", error);
      }
    };

    if (accounts && accounts.length > 0) {
      fetchAccessToken();
    }
  }, [instance, accounts]);

  return accessToken;
};
