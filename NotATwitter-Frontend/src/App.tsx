import * as React from "react";
import { Container } from "react-bootstrap";
import SearchForm from "./SearchForm";
import Header from "./Header";
import Message from "./Message";

export default function App() {
    return (
        <div>
            <Header/>
            <Container>
                <SearchForm className={"mt-2 mb-5"}/>

                {Array.from({ length: 100 }).map(() => {
                    return <Message className="my-3"/>;
                })}
            </Container>
        </div>
    );
}