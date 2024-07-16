import { createBrowserRouter } from "react-router-dom";
import Dashboard from "../pages/Dashboard";
import Login from "../pages/Login";
import AuthenticateUser from "../components/Login/AuthenticateUser";
import CustomError from "../pages/CustomError";
import NavBar from "../components/NavBar/NavBar";
import CustomerProfile from "../pages/CustomerProfile";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <AuthenticateUser />,
    children: [
      {
        path: "/",
        element: <NavBar />,
        children: [
          {
            path: "/",
            element: <Dashboard />,
          },
          {
            path: "/customer-profile/:profileId",
            element: <CustomerProfile />,
          },
        ],
      },
    ],
    errorElement: <CustomError />,
  },
  {
    path: "/login",
    element: <Login />,
  },
]);
