import { Spinner } from "react-bootstrap";
import React from "react";

export default function Loader() {
  const [currentStep, setCurrentStep] = React.useState(0);
  const steps = [
    "Checking Data",
    "Generating Compliance Token from Zatca",
    "Generating required Xmls",
    "Performing Compliance Check",
    "Generating Prod Token",
    "Almost there",
  ];

  React.useEffect(() => {
    const intervalId = setInterval(() => {
      if (currentStep === steps.length - 1) {
        setCurrentStep(0);
        return;
      }
      setCurrentStep((prevStep) => prevStep + 1);
    }, 1000);

    // Clear the interval when the component unmounts
    return () => clearInterval(intervalId);
  }, []);

  return (
    <div
      className="d-flex justify-content-center align-items-center"
      style={{ height: "200px" }}
    >
      <div className="d-flex flex-column align-items-center">
        <Spinner animation="grow" />
        <h6 className="mt-3">{steps[currentStep]}</h6>
      </div>
    </div>
  );
}
