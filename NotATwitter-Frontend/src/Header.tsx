import { Button, Col, Container, Navbar } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import * as React from "react";

const Header = () => {
    return (
        <Navbar bg="dark" variant="dark">
            <Container>
                <Col>
                    <LinkContainer to="/">
                        <Navbar.Brand>
                            NotATwitter
                        </Navbar.Brand>
                    </LinkContainer>
                </Col>
                <Col sm="auto">
                    <Button variant="secondary" size="sm" className="mx-1">Sign in</Button>
                </Col>

                <Col sm="auto">
                    <Button variant="primary" size="sm" className="mx-1">Sign up</Button>
                </Col>

            </Container>
        </Navbar>
    );
}

export default Header;