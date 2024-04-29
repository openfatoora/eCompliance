import React, { useState } from "react";
import { Form } from "react-bootstrap";

const Address = (props) => {
  const {
    streetName,
    setStreetName,
    additionalNo,
    setAdditionalNo,
    buildingNumber,
    setBuildingNumber,
    city,
    setCity,
    state,
    setState,
    zipCode,
    setZipCode,
    district,
    setDistrict,
    countryCode,
    setCountryCode,
  } = props;

  return (
    <>
      <Form.Group className="mb-3" controlId="exampleForm.ControlInput11">
        <Form.Label>Street Name</Form.Label>
        <Form.Control
          type="text"
          placeholder="street name"
          value={streetName}
          onChange={(e) => setStreetName(e.target.value)}
        />
      </Form.Group>

      {/* <Form.Group className="mb-3" controlId="exampleForm.ControlInput12">
                <Form.Label>Additional No</Form.Label>
                <Form.Control
                    type="text"
                    placeholder="1234"
                    value={additionalNo}
                    onChange={(e) => setAdditionalNo(e.target.value)}
                />
            </Form.Group> */}

      <Form.Group className="mb-3" controlId="exampleForm.ControlInput13">
        <Form.Label>Building Number</Form.Label>
        <Form.Control
          type="text"
          placeholder="1234"
          value={buildingNumber}
          onChange={(e) => setBuildingNumber(e.target.value)}
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="exampleForm.ControlInput14">
        <Form.Label>City</Form.Label>
        <Form.Control
          type="text"
          placeholder="Riyadh"
          value={city}
          onChange={(e) => setCity(e.target.value)}
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="exampleForm.ControlInput15">
        <Form.Label>State</Form.Label>
        <Form.Control
          type="text"
          placeholder="Riyadh"
          value={state}
          onChange={(e) => setState(e.target.value)}
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="exampleForm.ControlInput16">
        <Form.Label>Zip Code</Form.Label>
        <Form.Control
          type="text"
          placeholder="12345"
          value={zipCode}
          onChange={(e) => setZipCode(e.target.value)}
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="exampleForm.ControlInput17">
        <Form.Label>District</Form.Label>
        <Form.Control
          type="text"
          placeholder="fgffA"
          value={district}
          onChange={(e) => setDistrict(e.target.value)}
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="exampleForm.ControlInput4">
        <Form.Label>Country Code</Form.Label>
        <Form.Control
          type="text"
          placeholder="SA"
          value={countryCode}
          onChange={(e) => setCountryCode(e.target.value)}
        />
      </Form.Group>
    </>
  );
};

export default Address;
