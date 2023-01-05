import * as React from "react";
import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import App from "./App";
import "bootstrap/dist/css/bootstrap.min.css";
import SearchPage from "./SearchPage";

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
                element: <div>reg</div>
            },
            {
                path: "authorization",
                element: <div>auth</div>
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