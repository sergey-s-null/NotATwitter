import { Button, Card, Col, Form, InputGroup, Row } from "react-bootstrap";
import * as React from "react";


const SearchForm = (props: React.HTMLAttributes<HTMLElement>) => {
    return (
        <Card {...props}>
            <Card.Body>
                <Form>
                    <Form.Group className="mb-3">
                        <Form.Label>Search request</Form.Label>
                        <Form.Control type="search" size="sm"></Form.Control>
                    </Form.Group>

                    <Form.Group as={Row} className="mb-3">
                        <Form.Label column xs="auto">Show messages only for the last</Form.Label>
                        <Col style={{ height: "fit-content", margin: "auto" }}>
                            <InputGroup>
                                <Form.Control type="input" placeholder="...42..." size="sm"></Form.Control>
                                <Form.Select size="sm" style={{ maxWidth: "200px" }}>
                                    <option>minutes</option>
                                    <option>hours</option>
                                    <option>days</option>
                                    <option>weeks</option>
                                </Form.Select>
                            </InputGroup>
                        </Col>
                    </Form.Group>


                    <Form.Group className="mb-3">
                        <Form.Check label="Show only my own messages"></Form.Check>
                    </Form.Group>

                    <Button variant="primary" className="d-block mx-auto">Search</Button>
                </Form>
            </Card.Body>
        </Card>
    )
}

export default SearchForm;