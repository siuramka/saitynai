import { useContext } from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import LoginPage from "./components/LoginPage/LoginPage";
import { AuthContext } from "./utils/context/AuthContext";
import RegisterPage from "./components/RegisterPage.tsx/RegisterPage";
import ShopsList from "./components/Shops/ShopsList";
import SellerDashboard from "./components/Dashboard/Seller/SellerDashboard";
import AdminDashboard from "./components/Dashboard/Admin/AdminDashboard";
import UserDashboard from "./components/Dashboard/User/UserDashboard";
import Layout from "./layouts/Layout";
import LayoutManager from "./layouts/LayoutManager";
import SoftwaresList from "./components/ShopView/SoftwaresList";
import { Box, CssBaseline, ThemeProvider, createTheme } from "@mui/material";
import SoftwareView from "./components/SoftwareView/SoftwareView";
import SubscriptionList from "./components/Dashboard/Subscriptions/SubscriptionList";

function App() {
  const { user } = useContext(AuthContext);
  const darkTheme = createTheme({
    palette: {
      mode: "dark",
    },
  });
  return (
    <>
      <ThemeProvider theme={darkTheme}>
        <CssBaseline />
        {user ? (
          <>
            <LayoutManager role={user.role}>
              <Routes>
                <Route path="/dashboard">
                  {user.role === "ShopUser" && (
                    <>
                      <Route index element={<UserDashboard />} />
                      <Route path="subscriptions" element={<SubscriptionList />} />
                      <Route path="shops" element={<ShopsList />} />
                      <Route path="shops/:id" element={<SoftwaresList />} />
                      <Route
                        path="shops/:shopId/softwares/:softwareId"
                        element={<SoftwareView />}
                      />
                    </>
                  )}
                  {user.role === "ShopSeller" && (
                    <>
                      <Route index element={<SellerDashboard />} />
                      <Route path="shops" element={<ShopsList />} />
                      <Route path="shops/:id" element={<SoftwaresList />} />
                      <Route path="shops/:id/edit" element={<> shops edit</>} />
                      <Route
                        path="softwares/:id/edit"
                        element={<> softwares edit</>}
                      />
                    </>
                  )}
                  {user.role === "Admin" && (
                    <Route index element={<AdminDashboard />} />
                  )}
                </Route>
              </Routes>
            </LayoutManager>
          </>
        ) : (
          <>
            <Layout>
              <Routes>
                <Route path="*" element={<Navigate to="/sign-in" replace />} />
                <Route path="/sign-in" element={<LoginPage />} />
                <Route path="/sign-up" element={<RegisterPage />} />
              </Routes>
            </Layout>
          </>
        )}
      </ThemeProvider>
    </>
  );
}

export default App;
