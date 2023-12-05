import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { BrowserRouter } from "react-router-dom";
import { configureStore } from "@reduxjs/toolkit";

import cartReducer from "./features/CartSlice.ts";
import { Provider } from "react-redux";
import { NotificationContextProvider } from "./utils/context/NotificationContext.tsx";
import { LoaderContextProvider } from "./utils/context/LoaderContext.tsx";
import { store } from "./app/store.tsx";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <Provider store={store}>
    <LoaderContextProvider>
      <NotificationContextProvider>
        <React.StrictMode>
          <BrowserRouter>
            <App />
          </BrowserRouter>
        </React.StrictMode>
      </NotificationContextProvider>
    </LoaderContextProvider>
  </Provider>
);
