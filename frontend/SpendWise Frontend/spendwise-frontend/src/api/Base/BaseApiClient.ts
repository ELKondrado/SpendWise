import axios from "axios";

const defaultHeaders = {
  "Content-Type": "application/json",
};

export const SpendWiseClient = axios.create({
  baseURL: "https://localhost:7204/api/",
  headers: defaultHeaders,
});
