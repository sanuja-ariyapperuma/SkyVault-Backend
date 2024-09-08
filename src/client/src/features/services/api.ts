import axios, { AxiosResponse, InternalAxiosRequestConfig } from "axios";
import { baseURL, scopes } from "../Helpers/helper";
import { notifyError } from "../../components/CommonComponents/Toasters";
import { msalInstance } from "../../main";

const api = axios.create({
  baseURL: baseURL,
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
  },
});

// Request interceptor
api.interceptors.request.use(
  async (config: InternalAxiosRequestConfig) => {
    const response = await msalInstance.acquireTokenSilent({
      scopes: [scopes],
      account: msalInstance.getActiveAccount() ?? undefined,
    });

    const accessToken = response.accessToken;

    if (accessToken) config.headers["Authorization"] = `Bearer ${accessToken}`;
    console.log("Bearer ", accessToken);
    return config;
  },
  (error) => {
    console.error("Error making Axios request:", error);
    return Promise.reject(error);
  }
);

// Response interceptor
api.interceptors.response.use(
  (response: AxiosResponse) => {
    return response;
  },
  (error) => {
    if (error.response && error.response.status === 401) {
      notifyError("Unauthorized action");
      window.location.href = "/login";
    } else {
      notifyError(
        "Something went wrong. Please try again later or contact system administrator"
      );
    }

    return Promise.reject(error);
  }
);

export default api;
