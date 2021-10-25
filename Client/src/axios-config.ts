import axios from "axios"
//Used to send authentication tokens on HTTP requests when appropriate, and also sets the base URL depending on environment.
let apiUrl = process.env.REACT_APP_API_URL
if (apiUrl === "") apiUrl = "https://in-team-app.herokuapp.com/api/"
const instance = axios.create({
  baseURL: apiUrl,
})

instance.interceptors.request.use(
  function (config) {
    const token = localStorage.getItem("token")
    if (token) {
      config.headers["Authorization"] = `Bearer ${token}`
    }
    return config
  },
  function (error) {
    return Promise.reject(error)
  }
)

export default instance
