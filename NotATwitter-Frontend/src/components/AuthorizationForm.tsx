import * as React from "react";
import { Button, Card, FloatingLabel, Form } from "react-bootstrap";

const AuthorizationForm = () => {
    return (
        <Card className="mx-auto mt-5" style={{ maxWidth: "400px" }}>
            <Card.Body>
                <Form>
                    <h3 className="text-center mb-3">Authorization</h3>
                    <Form.FloatingLabel label="Name" className="mb-3">
                        <Form.Control type="input" placeholder="Name" size="sm"/>
                    </Form.FloatingLabel>
                    <FloatingLabel label="Password" className="mb-3">
                        <Form.Control type="password" placeholder="Password"/>
                    </FloatingLabel>
                    <Button className="d-block mx-auto">Sing in</Button>
                </Form>
            </Card.Body>
        </Card>
    );
}

export default AuthorizationForm;