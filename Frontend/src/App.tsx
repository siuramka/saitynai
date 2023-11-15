import { useContext } from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import LoginPage from "./components/LoginPage/LoginPage";
import { AuthContext } from "./utils/context/AuthContext";
import UserDashboard from "./components/Dashboard/UserDashboard";
import SellerDashboard from "./components/Dashboard/SellerDashboard";
import AdminDashboard from "./components/Dashboard/AdminDashboard";

function App() {
  const { user } = useContext(AuthContext);

  return (
    <Routes>
      {user ? (
        <>
          <Route path="*" element={<Navigate to="/dashboard" replace />} />
          {user.role === "ShopUser" && (
            <>
              <Route path="/dashboard" element={<UserDashboard />} />
            </>
          )}
          {user.role === "ShopSeller" && (
            <>
              <Route path="/dashboard" element={<SellerDashboard />} />
            </>
          )}
          {user.role === "Admin" && (
            <>
              <Route path="/dashboard" element={<AdminDashboard />} />
            </>
          )}
        </>
      ) : (
        <>
          <Route path="*" element={<Navigate to="/sign-in" replace />} />
          <Route path="/sign-in" element={<LoginPage />} />
        </>
      )}
    </Routes>
  );
}

export default App;
