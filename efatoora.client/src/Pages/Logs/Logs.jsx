import React from "react";
import Table from "react-bootstrap/Table";
import {
  Card,
  Form,
  Row,
  Col,
  Button,
  InputGroup,
  Popover,
  OverlayTrigger,
  Spinner,
  Pagination,
} from "react-bootstrap";
import "react-date-range/dist/styles.css";
import "react-date-range/dist/theme/default.css";
import { DateRangePicker } from "react-date-range";
import dayjs from "dayjs";
import axios from "axios";

const Logs = () => {
  const [dateRange, setDateRange] = React.useState({
    startDate: dayjs().startOf("week").toDate(),
    endDate: dayjs().toDate(),
    key: "selection",
  });
  const [invoiceLogs, setInvoiceLogs] = React.useState([]);
  const [isLoading, setIsLoading] = React.useState(false);
  const [isError, setIsError] = React.useState(false);
  const [currentPage, setCurrentPage] = React.useState(1);
  const itemsPerPage = 10;
  const totalPages = Math.ceil(invoiceLogs.length / itemsPerPage);

  const currentItems = invoiceLogs.slice(
    (currentPage - 1) * itemsPerPage,
    currentPage * itemsPerPage
  );

  const handleSearch = () => {
    setIsError(false);
    setIsLoading(true);
    axios
      .get(
        `/Log/GetInvoiceLogs?fromDate=${dayjs(dateRange.startDate).format(
          "MM-DD-YYYY"
        )}&toDate=${dayjs(dateRange.endDate).format("MM-DD-YYYY")}`
      )
      .then((response) => {
        if (response.status === 200) {
          setIsLoading(false);
          setInvoiceLogs(response.data);
        }
      })
      .catch((error) => {
        setIsLoading(false);
        setIsError(true);
        console.log(error);
      });
  };

  const handleSelect = (ranges) => {
    var startDate = ranges.selection.startDate;
    var endDate = ranges.selection.endDate;
    setDateRange({ startDate, endDate, key: "selection" });
  };

  const popover = (
    <Popover id="popover-basic" style={{ minWidth: " 580px" }}>
      <Popover.Body>
        <DateRangePicker ranges={[dateRange]} onChange={handleSelect} />
      </Popover.Body>
    </Popover>
  );

  return (
    <Card
      className="mt-5"
      style={{ width: "50rem", minHeight: "300px", margin: "auto" }}
    >
      <Card.Body>
        <InputGroup className="mb-3">
          <OverlayTrigger
            trigger="click"
            placement="bottom"
            overlay={popover}
            rootClose
          >
            <Form.Control
              type="text"
              value={`📅  ${dayjs(dateRange.startDate).format(
                "YYYY-MM-DD"
              )} - ${dayjs(dateRange.endDate).format("YYYY-MM-DD")}`}
              readOnly
            />
          </OverlayTrigger>

          <Button variant="primary" onClick={handleSearch}>
            Search
          </Button>
        </InputGroup>

        <>
          {isLoading ? (
            <div
              className="d-flex justify-content-center align-items-center"
              style={{ height: "200px" }}
            >
              <Spinner animation="grow" />
            </div>
          ) : isError ? (
            <div className="text-center" style={{ paddingTop: "40px" }}>
              <h5>Oops! Something went wrong.</h5>
            </div>
          ) : invoiceLogs.length === 0 ? (
            <div className="text-center" style={{ paddingTop: "40px" }}>
              <h5>Oops! no data found for the selected range.</h5>
            </div>
          ) : (
            <>
              <Table striped bordered hover>
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>Invoice Number</th>
                    <th>Operation</th>
                    <th>Status</th>
                    <th>Date</th>
                  </tr>
                </thead>
                <tbody>
                  {currentItems.map((log) => (
                    <tr key={log.id}>
                      <td>{log.id}</td>
                      <td>{log.invoiceNo}</td>
                      <td>{log.operationType}</td>
                      <td>{log.status}</td>
                      <td>
                        {dayjs(log.dateTime).format("YYYY-MM-DD HH:mm:ss")}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </Table>
              <Pagination>
                {[...Array(totalPages).keys()].map((page) => (
                  <Pagination.Item
                    key={page + 1}
                    active={page + 1 === currentPage}
                    onClick={() => setCurrentPage(page + 1)}
                  >
                    {page + 1}
                  </Pagination.Item>
                ))}
              </Pagination>
            </>
          )}
        </>
      </Card.Body>
    </Card>
  );
};

export default Logs;
