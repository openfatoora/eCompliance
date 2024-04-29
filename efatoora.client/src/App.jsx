import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import OnBoarding from "./Pages/OnBoarding/OnBoarding";
import DashBoard from "./Pages/DashBoard/DashBoard";
import Logs from "./Pages/Logs/Logs";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<DashBoard />} />
        <Route path="/register" element={<OnBoarding />} />
        <Route path="/Logs" element={<Logs />} />
      </Routes>
    </Router>
  );
}

export default App;
