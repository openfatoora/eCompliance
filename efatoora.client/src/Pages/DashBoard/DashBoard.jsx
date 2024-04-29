import React from "react";
import axios from "axios";
import { Card, Form, Row, Col } from "react-bootstrap";
import dayjs from "dayjs";

function DashBoard() {
  const [viewModel, setViewModel] = React.useState({});
  const [hideSecret, setHideSecret] = React.useState(true);

  React.useEffect(() => {
    const fetchData = async () => {
      axios
        .get("/Dashboard/ViewModel")
        .then((response) => {
          if (response.status === 200) {
            setViewModel(response.data);
          }
          if (response.data.onboardingResult.isOnBoarded === "true") {
            window.location.href = "/register";
          }
          console.log(response.data);
        })
        .catch((error) => {
          console.log(error);
        });
    };

    fetchData();
  }, []);

  return (
    <>
      {viewModel && viewModel.onboardingResult && (
        <Card
          className="mt-5"
          style={{ width: "50rem", height: "350px", margin: "auto" }}
        >
          <Card.Title>
            <h4 className="px-2 pt-2">Device Info</h4>
          </Card.Title>
          <Card.Body>
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm="2">
                Binary Token
              </Form.Label>
              <Col sm="10">
                <Form.Control
                  type={hideSecret ? "password" : "text"}
                  disabled
                  value={viewModel.onboardingResult.binaryToken}
                />
              </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm="2">
                Private Key
              </Form.Label>
              <Col sm="10">
                <Form.Control
                  type={hideSecret ? "password" : "text"}
                  disabled
                  value={viewModel.onboardingResult.privateKey}
                />
              </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm="2">
                Secret
              </Form.Label>
              <Col sm="10">
                <Form.Control
                  type={hideSecret ? "password" : "text"}
                  disabled
                  value={viewModel.onboardingResult.secret}
                />
              </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm="2">
                Created Time
              </Form.Label>
              <Col sm="10">
                <Form.Control
                  type="text"
                  disabled
                  value={dayjs(viewModel.onboardingResult.updatedAt).format(
                    "YYYY-MM-DD HH:mm:ss"
                  )}
                />
              </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm="2">
                Expires In
              </Form.Label>
              <Col sm="10">
                <Form.Control
                  type="text"
                  disabled
                  value={`${dayjs(viewModel.onboardingResult.updatedAt)
                    .add(1, "year")
                    .diff(
                      dayjs(viewModel.onboardingResult.updatedAt),
                      "day"
                    )} days`}
                />
              </Col>
            </Form.Group>
          </Card.Body>
          <Card.Footer className="bg-grey" style={{ borderTop: "none" }}>
            <button
              style={{ width: "140px", height: "40px" }}
              className="btn btn-primary"
              onClick={() => setHideSecret(!hideSecret)}
            >
              {hideSecret ? "👁️ Reveal" : "👁️ Hide  "}
            </button>
            <button
              style={{ width: "140px", height: "40px" }}
              className="btn btn-primary mx-2"
              onClick={() => (window.location.href = "/swagger/index.html")}
            >
              Api Docs
            </button>
            <button
              style={{ width: "140px", height: "40px" }}
              className="btn btn-primary"
              onClick={() => (window.location.href = "/Logs")}
            >
              Logs
            </button>
            <button
              style={{ width: "140px", height: "40px" }}
              className="btn btn-primary mx-2"
              onClick={() => (window.location.href = "/register")}
            >
              Onboard Again
            </button>
            <button
              style={{ width: "140px", height: "40px" }}
              className="btn btn-primary"
              onClick={() => window.alert("Coming soon")}
            >
              Reniew
            </button>
          </Card.Footer>
        </Card>
      )}
    </>
  );
}

export default DashBoard;
