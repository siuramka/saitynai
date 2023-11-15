import axios from "axios";

const baseURL = "http://localhost:5000/api/";

const api = axios.create({
  baseURL,
});

api.interceptors.request.use(
  function (config) {
    const token = localStorage.getItem("token"); // Assuming you store your token in localStorage
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  function (error) {
    console.log("failed token from storatge");
    return Promise.reject(error);
  }
);

export default api;
