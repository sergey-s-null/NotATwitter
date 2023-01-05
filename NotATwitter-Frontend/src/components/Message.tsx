import * as React from "react";
import { Card } from "react-bootstrap";

const Message = (props: React.HTMLAttributes<HTMLElement>) => {
    return (
        <Card {...props}>
            <Card.Header>
                Header
            </Card.Header>
            <Card.Body>
                This is body
            </Card.Body>
        </Card>
    );
}

export default Message;