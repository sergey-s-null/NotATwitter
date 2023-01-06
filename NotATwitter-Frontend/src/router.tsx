import * as React from "react";
import { createBrowserRouter, createRoutesFromElements, Route } from "react-router-dom";
import App from "./App";
import SearchPage from "./components/SearchPage";
import RegistrationForm from "./components/RegistrationForm";
import AuthorizationForm from "./components/AuthorizationForm";

const router = createBrowserRouter(
    createRoutesFromElements(
        <Route path="/" element={<App/>}>
            <Route path="" element={<SearchPage/>}/>
            <Route path="registration" element={<RegistrationForm/>}/>
            <Route path="authorization" element={<AuthorizationForm/>}/>
        </Route>
    )
);

export default router;