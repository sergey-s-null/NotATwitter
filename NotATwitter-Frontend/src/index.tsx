import * as React from "react";
import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import App from "./App";
import "bootstrap/dist/css/bootstrap.min.css";
import SearchPage from "./components/SearchPage";
import AuthorizationForm from "./components/AuthorizationForm";
import RegistrationForm from "./components/RegistrationForm";

const router = createBrowserRouter([
    {
        path: "/",
        element: <App/>,
        children: [
            {
                path: "",
                element: <SearchPage/>
            },
            {
                path: "registration",
                element: <RegistrationForm/>
            },
            {
                path: "authorization",
                element: <AuthorizationForm/>
            }
        ]
    }
]);


const root = createRoot(document.getElementById("root"));
root.render(
    <React.StrictMode>
        <RouterProvider router={router}/>
    </React.StrictMode>
);