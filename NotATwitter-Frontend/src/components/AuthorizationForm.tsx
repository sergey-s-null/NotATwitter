import * as React from "react";
import { Button, Card, FloatingLabel, FormControl } from "react-bootstrap";
import { Form as RouterForm } from "react-router-dom";


const AuthorizationForm = () => {
    return (
        <Card className="mx-auto mt-5" style={{ maxWidth: "400px" }}>
            <Card.Body>
                <RouterForm method="post">
                    <h3 className="text-center mb-3">Authorization</h3>
                    <FloatingLabel label="Name" className="mb-3">
                        <FormControl type="input" placeholder="Name" size="sm"/>
                    </FloatingLabel>
                    <FloatingLabel label="Password" className="mb-3">
                        <FormControl type="password" placeholder="Password"/>
                    </FloatingLabel>
                    <Button type="submit" className="d-block mx-auto">Sing in</Button>
                </RouterForm>
            </Card.Body>
        </Card>
    );
}

export default AuthorizationForm;