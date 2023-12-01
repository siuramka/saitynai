import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { BrowserRouter } from "react-router-dom";
import { AuthContextProvider } from "./utils/context/AuthContext.tsx";
import { configureStore } from "@reduxjs/toolkit";

import cartReducer from "./features/CartSlice.ts";
import { Provider } from "react-redux";
import { NotificationContextProvider } from "./utils/context/NotificationContext.tsx";
import { LoaderContextProvider } from "./utils/context/LoaderContext.tsx";

const store = configureStore({
  reducer: {
    cart: cartReducer,
  },
});

ReactDOM.createRoot(document.getElementById("root")!).render(
  <Provider store={store}>
    <LoaderContextProvider>
      <NotificationContextProvider>
        <AuthContextProvider>
          <React.StrictMode>
            <BrowserRouter>
              <App />
            </BrowserRouter>
          </React.StrictMode>
        </AuthContextProvider>
      </NotificationContextProvider>
    </LoaderContextProvider>
  </Provider>
);
