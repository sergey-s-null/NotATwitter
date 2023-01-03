import React from "react";
import ReactDOM from "react-dom";
// todo что не так с Switch?
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";

import App from "./App.js";
// import "./styles.scss";

const appRouting = (
    <Router>
        <Switch>
            <Route exact path="/" component={App} />
        </Switch>
    </Router>
);

ReactDOM.render(appRouting, document.getElementById("root"));