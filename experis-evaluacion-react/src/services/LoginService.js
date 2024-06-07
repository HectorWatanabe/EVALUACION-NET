import axios from "axios";

export const LoginService = (email, password) =>
    axios.post(`${process.env.REACT_APP_API_URL}Login`, { "email": email, "password": password });
