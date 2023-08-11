import { RouteObject, createBrowserRouter } from "react-router-dom";
import App from "../../App";
import HomePage from "../features/home/HomePage";
import AppUserRegister from "../features/user/dashboard/appUser.register";
import AppUserDashboard from "../features/user/dashboard/appUser.dashboard";


export const routes : RouteObject[]= [
    {
        path: '/',
        element: <App/>,
        children: [
            {path: '', element: <HomePage/>},
            {path: 'appUsers', element: <AppUserDashboard/>},
            {path: 'registerUser', element: <AppUserRegister/>}
        ]
    }

]
export const router= createBrowserRouter(routes);


