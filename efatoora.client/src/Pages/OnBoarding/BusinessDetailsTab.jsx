import { Form, Row, Col } from "react-bootstrap";

const BusinessDetailsTab = (props) => {
  const {
    businessName,
    setBusinessName,
    deviceName,
    setDeviceName,
    industry,
    setIndustry,
    additionalIdType,
    setAdditionalIdType,
    additionalIdNumber,
    setAdditionalIdNumber,
    vat,
    setVat,
    setGroupVatNumber,
  } = props;

  return (
    <>
      <Form.Group className="mb-3" controlId="exampleForm.ControlInput3">
        <Form.Label>Business Name</Form.Label>
        <Form.Control
          type="text"
          value={businessName}
          onChange={(e) => setBusinessName(e.target.value)}
          placeholder="Infinite"
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="exampleForm.ControlInput2">
        <Form.Label>Device Name</Form.Label>
        <Form.Control
          type="text"
          value={deviceName}
          onChange={(e) => setDeviceName(e.target.value)}
          placeholder="Unique Name"
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="exampleForm.ControlInput6">
        <Form.Label>Industry</Form.Label>
        <Form.Control
          type="text"
          placeholder="Software"
          value={industry}
          onChange={(e) => setIndustry(e.target.value)}
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="exampleForm.ControlInput7">
        <Form.Label>Additional ID Type</Form.Label>
        <Form.Select
          value={additionalIdType}
          onChange={(e) => setAdditionalIdType(e.target.value)}
        >
          <option value="CRN">CRN</option>
          <option value="MOM">MOM</option>
          <option value="MLS">MLS</option>
          <option value="SAG">SAG</option>
          <option value="OTH">OTH</option>
        </Form.Select>
      </Form.Group>

      <Form.Group className="mb-3" controlId="exampleForm.ControlInput8">
        <Form.Label>Additional ID Number</Form.Label>
        <Form.Control
          type="text"
          placeholder="1234"
          value={additionalIdNumber}
          onChange={(e) => setAdditionalIdNumber(e.target.value)}
        />
      </Form.Group>

      <Row>
        <Col>
          <Form.Group className="mb-3" controlId="exampleForm.ControlInput9">
            <Form.Label>VAT</Form.Label>
            <Form.Control
              type="text"
              placeholder="310175397400003"
              value={vat}
              onChange={(e) => setVat(e.target.value)}
            />
          </Form.Group>
        </Col>
        <Col className="d-flex align-items-center">
          <Form.Group
            className="mb-2 pt-3"
            controlId="exampleForm.ControlInput10"
          >
            <Form.Check
              type="switch"
              id="custom-switch"
              label="Is Group VAT Number"
              onChange={(e) => {
                if (e.target.checked) {
                  setGroupVatNumber(vat);
                }
              }}
            />
          </Form.Group>
        </Col>
      </Row>
    </>
  );
};

export default BusinessDetailsTab;
