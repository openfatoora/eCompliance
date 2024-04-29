import { Form } from "react-bootstrap";
import OtpInput from "react-otp-input";

const Compliance = (props) => {
  const {
    environment,
    setEnvironment,
    supportedInvoiceTypes,
    setInvoiceTypes,
    otp,
    setOtp,
  } = props;

  return (
    <>
      <Form.Group className="mb-3" controlId="exampleForm.ControlInput18">
        <Form.Label>Environment</Form.Label>
        <Form.Control
          type="text"
          placeholder="SandBox"
          value={environment}
          onChange={(e) => setEnvironment(e.target.value)}
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="exampleForm.ControlInput5">
        <Form.Label>Supported Invoice Types</Form.Label>
        <Form.Control
          type="text"
          placeholder="0100"
          value={supportedInvoiceTypes}
          onChange={(e) => setInvoiceTypes(e.target.value)}
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
        <Form.Label>OTP</Form.Label>
        <OtpInput
          value={otp}
          onChange={(otp) => setOtp(otp)}
          numInputs={6}
          renderSeparator={<span> </span>}
          renderInput={(props) => <input {...props} />}
          inputStyle={{
            width: "50px",
            height: "50px",
            margin: "0 12px",
            borderRadius: "6px",
            border: "1px solid #ced4da",
            textAlign: "center",
            fontSize: "20px",
            color: "black",
            outline: "none",
          }}
        />
      </Form.Group>
    </>
  );
};

export default Compliance;
