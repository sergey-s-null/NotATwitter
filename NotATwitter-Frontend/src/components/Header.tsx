import { Button, Col, Container, InputGroup, Navbar } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import * as React from "react";

const Header = () => {
    const authorized = true;

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

                {authorized &&
                    <Col sm="auto" className="me-3">
                        <Button
                            variant="success"
                            size="sm"
                            style={{ padding: "0px 6px" }}
                        >
                            <i
                                className="bi bi-plus"
                                style={{ color: "white", fontSize: "20px" }}
                            />
                        </Button>
                    </Col>
                }

                {authorized &&
                    <Col sm="auto">
                        <InputGroup>
                            <Button
                                variant="outline-light"
                                size="sm"
                            >
                                Some user
                            </Button>
                            <Button
                                variant="light"
                                size="sm"
                                style={{ padding: "0px 6px" }}
                            >
                                <i
                                    className="bi bi-box-arrow-right"
                                    style={{ fontSize: "20px" }}
                                />
                            </Button>
                        </InputGroup>
                    </Col>
                }

                {!authorized &&
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
                }

                {!authorized &&
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
                }
            </Container>
        </Navbar>
    );
}

export default Header;