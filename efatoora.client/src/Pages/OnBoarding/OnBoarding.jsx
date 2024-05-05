import React from "react";
import axios from "axios";
import { useState } from "react";
import { Card, Button, Alert } from "react-bootstrap";
import Tab from "react-bootstrap/Tab";
import Tabs from "react-bootstrap/Tabs";
import BusinessDetailsTab from "./BusinessDetailsTab";
import Address from "./Address";
import Compliance from "./Compliance";
import Loader from "./Loader";
import {useNavigate} from 'react-router-dom'

export default function OnBoarding() {
  const naviagate = useNavigate();
  const [otp, setOtp] = useState("000000");
  const [deviceName, setDeviceName] = useState("Unique Name");
  const [businessName, setBusinessName] = useState("Infinite");
  const [countryCode, setCountryCode] = useState("SA");
  const [supportedInvoiceTypes, setInvoiceTypes] = useState("0100");
  const [streetName, setStreetName] = useState("street name");
  const [additionalNo, setAdditionalNo] = useState("1234");
  const [buildingNumber, setBuildingNumber] = useState("1234");
  const [city, setCity] = useState("Riyadh");
  const [industry, setIndustry] = useState("Software");
  const [additionalIdType, setAdditionalIdType] = useState("OTH");
  const [additionalIdNumber, setAdditionalIdNumber] = useState("1234");
  const [vat, setVat] = useState("310175397400003");
  const [groupVatNumber, setGroupVatNumber] = useState("310175397400003");
  const [state, setState] = useState("Riyadh");
  const [zipCode, setZipCode] = useState("12345");
  const [district, setDistrict] = useState("fgffA");
  const [environment, setEnvironment] = useState("SandBox");
  const [activeTab, setActiveTab] = useState("Business");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [isSuccess, setIsSuccess] = useState(false);
  const [isError, setIsError] = useState(false);

  const handleSubmit = () => {
    setIsSubmitting(true);
    const formData = {
      otp,
      deviceName,
      businessName,
      countryCode,
      supportedInvoiceTypes,
      streetName,
      additionalNo,
      buildingNumber,
      city,
      industry,
      additionalIdType,
      additionalIdNumber,
      vat,
      groupVatNumber,
      state,
      zipCode,
      district,
      environment,
    };

    axios
      .post("/onboard", formData)
      .then((response) => {
        setIsSubmitting(false);
        setIsSuccess(true);
        const timeoutId = setTimeout(() => {
          naviagate('/')
        }, 1000);
      })
      .catch((error) => {
        setIsSubmitting(false);
        setIsError(true);
      });
  };

  const handleNext = () => {
    let nextTab;
    switch (activeTab) {
      case "Business":
        nextTab = "Address";
        break;
      case "Address":
        nextTab = "Compliance";
        break;
      case "Compliance":
        handleSubmit();
        return;
      default:
        return;
    }
    setActiveTab(nextTab);
  };

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
      }}
    >
      <Card style={{ width: "50rem", height: "780px", margin: "auto" }}>
        <Card.Body>
          <Card.Title>Onboard your device to Zatca</Card.Title>
          <div>
            {isSubmitting ? (
              <Loader />
            ) : isSuccess ? (
              <div
                className="d-flex justify-content-center align-items-center"
                style={{ height: "200px" }}
              >
                <Alert variant="success">✅ Success!</Alert>
              </div>
            ) : isError ? (
              <div
                className="d-flex justify-content-center align-items-center"
                style={{ height: "200px" }}
              >
                <Alert variant="danger">❌ Error!</Alert>
              </div>
            ) : (
              <Tabs
                defaultActiveKey="Business"
                activeKey={activeTab}
                id="uncontrolled-tab-example"
                className="mb-3"
                fill={true}
                onSelect={(k) => setActiveTab(k)}
              >
                <Tab eventKey="Business" title="Business Details">
                  <BusinessDetailsTab
                    businessName={businessName}
                    setBusinessName={setBusinessName}
                    deviceName={deviceName}
                    setDeviceName={setDeviceName}
                    industry={industry}
                    setIndustry={setIndustry}
                    additionalIdType={additionalIdType}
                    setAdditionalIdType={setAdditionalIdType}
                    additionalIdNumber={additionalIdNumber}
                    setAdditionalIdNumber={setAdditionalIdNumber}
                    vat={vat}
                    setVat={setVat}
                    groupVatNumber={groupVatNumber}
                    setGroupVatNumber={setGroupVatNumber}
                  />
                </Tab>
                <Tab eventKey="Address" title="Address">
                  <Address
                    countryCode={countryCode}
                    setCountryCode={setCountryCode}
                    streetName={streetName}
                    setStreetName={setStreetName}
                    additionalNo={additionalNo}
                    setAdditionalNo={setAdditionalNo}
                    buildingNumber={buildingNumber}
                    setBuildingNumber={setBuildingNumber}
                    city={city}
                    setCity={setCity}
                    state={state}
                    setState={setState}
                    zipCode={zipCode}
                    setZipCode={setZipCode}
                    district={district}
                    setDistrict={setDistrict}
                  />
                </Tab>
                <Tab eventKey="Compliance" title="Compliance">
                  <Compliance
                    environment={environment}
                    setEnvironment={setEnvironment}
                    supportedInvoiceTypes={supportedInvoiceTypes}
                    setInvoiceTypes={setInvoiceTypes}
                    otp={otp}
                    setOtp={setOtp}
                  />
                </Tab>
              </Tabs>
            )}
          </div>
        </Card.Body>
        <Card.Footer className="d-flex justify-content-end bg-white">
          <Button variant="primary" onClick={handleNext}>
            {activeTab === "Compliance" ? "Submit" : "Next"}
          </Button>
        </Card.Footer>
      </Card>
    </div>
  );
}
