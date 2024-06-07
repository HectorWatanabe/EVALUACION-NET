import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import './App.css';
import UserPage from './pages/UserPage';
import NotFoundPage from './pages/NotFoundPage';
import LoginPage from "./pages/LoginPage";

function App() {
  return (
    <Router>
      <Routes>
        <Route exact path="/" element={<LoginPage />} />
        <Route exact path="/Bienvenido" element={<UserPage />} />
        <Route path="*" element={<NotFoundPage />} />
      </Routes>
    </Router>
  );
}

export default App;
