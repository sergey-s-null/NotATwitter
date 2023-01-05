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
                    <LinkContainer to="authorization">
                        <Button
                            variant="secondary"
                            size="sm"
                            className="mx-1"
                        >
                            Sign in
                        </Button>
                    </LinkContainer>
                </Col>

                <Col sm="auto">
                    <LinkContainer to="registration">
                        <Button
                            variant="primary"
                            size="sm"
                            className="mx-1"
                        >
                            Sign up
                        </Button>
                    </LinkContainer>
                </Col>
            </Container>
        </Navbar>
    );
}

export default Header;