import * as React from "react";
import { Container } from "react-bootstrap";
import SearchForm from "./SearchForm";
import Header from "./Header";

export default function App() {
    return (
        <div>
            <Header/>
            <Container>
                <SearchForm/>
            </Container>
        </div>
    );
}