import { useContext, useEffect } from "react";
import { Navigate, Route, Routes, useLocation } from "react-router-dom";
import LoginPage from "./components/LoginPage/LoginPage";
import { AuthContext } from "./utils/context/AuthContext";
import RegisterPage from "./components/RegisterPage.tsx/RegisterPage";
import ShopsList from "./components/Shops/ShopsList";
import SellerDashboard from "./components/Dashboard/Seller/SellerDashboard";
import AdminDashboard from "./components/Dashboard/Admin/AdminDashboard";
import UserDashboard from "./components/Dashboard/User/UserDashboard";
import Layout from "./layouts/Layout";
import LayoutManager from "./layouts/LayoutManager";
import { CssBaseline, ThemeProvider, createTheme } from "@mui/material";
import SoftwareView from "./components/SoftwareView/SoftwareView";
import SubscriptionList from "./components/Subscriptions/SubscriptionList";
import AdminShopsList from "./components/Shops/AdminShopsList";
import ShopView from "./components/ShopView/ShopView";
import AlertNotification from "./components/AlertNotification/AlertNotification";
import Loader from "./components/Loader/Loader";
import { LoaderContext } from "./utils/context/LoaderContext";
import AdminSoftwaresList from "./components/ShopView/AdminSoftwaresList";

function App() {
  const { user } = useContext(AuthContext);
  const { setLoaderHandler } = useContext(LoaderContext);
  const location = useLocation();

  const darkTheme = createTheme({
    palette: {
      mode: "dark",
    },
  });

  useEffect(() => {
    setLoaderHandler(true);
    setTimeout(() => {
      setLoaderHandler(false);
    }, 0);
  }, [location]);

  return (
    <>
      <ThemeProvider theme={darkTheme}>
        <CssBaseline />
        {user ? (
          <>
            <Loader />
            <AlertNotification />
            <LayoutManager role={user.role}>
              <Routes>
                <Route path="/dashboard">
                  {user.role === "ShopUser" && (
                    <>
                      <Route index element={<UserDashboard />} />
                      <Route
                        path="subscriptions"
                        element={<SubscriptionList />}
                      />
                      <Route path="shops" element={<ShopsList />} />
                      <Route path="shops/:id" element={<ShopView />} />
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
                      <Route path="shops/:id" element={<ShopView />} />
                      <Route
                        path="shops/:shopId/softwares/:softwareId"
                        element={<SoftwareView />}
                      />
                    </>
                  )}
                  {user.role === "Admin" && (
                    <>
                      <Route index element={<AdminDashboard />} />
                      <Route path="shops" element={<AdminShopsList />} />
                      <Route
                        path="softwares"
                        element={<AdminSoftwaresList />}
                      />
                    </>
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
