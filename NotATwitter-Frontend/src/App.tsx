import * as React from "react";
import { Container } from "react-bootstrap";
import Header from "./Header";
import { Outlet } from "react-router-dom";

export default function App() {
    return (
        <div>
            <Header/>
            <Container>
                <Outlet/>
            </Container>
        </div>
    );
}