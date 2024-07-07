import { createBrowserRouter } from "react-router-dom";
import Dashboard from "../pages/Dashboard";
import Login from "../pages/Login";
import AuthenticateUser from "../components/Login/AuthenticateUser";
import CustomError from "../pages/CustomError";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <AuthenticateUser />,
    children: [
      {
        path: "/",
        element: <Dashboard />,
      },
    ],
    errorElement: <CustomError />,
  },
  {
    path: "/login",
    element: <Login />,
  },
]);
