import axios, { AxiosRequestConfig, Method } from "axios";
import { useSelector } from "react-redux";
import { RootState } from "../../store";

export const baseURL = "https://localhost:7199";

const axiosInstance = axios.create({
  baseURL: "https://localhost:7199", // Replace with your base URL
  headers: {
    "Content-Type": "application/json",
  },
});

const setAuthToken = () => {
  const accessToken = useSelector(
    (store: RootState) => store.tokenR.accessToken
  );
  axiosInstance.defaults.headers.common[
    "Authorization"
  ] = `Bearer ${accessToken}`;
};

export interface RequestParams {
  url: string;
  method: Method;
  body?: any;
}

const axiosRequest = async ({ url, method, body }: RequestParams) => {
  const config: AxiosRequestConfig = {
    url,
    method,
    data: body,
  };

  setAuthToken();

  try {
    const response = await axiosInstance(config);
    return response.data;
  } catch (error) {
    // Handle error
    console.error("Error making Axios request:", error);
    throw error;
  }
};

export { axiosRequest };
