import Login from "../components/authentication/Login"
import Register from "../components/authentication/Register"
import LandingPage from "../components/landing/LandingPage"
import IRoute from "./IRoute"

const unAuthenticatedRoutes: IRoute[] = [
  {
    path: "/login",
    name: "Login page",
    exact: true,
    component: Login,
  },

  {
    path: "/register",
    name: "Register page",
    exact: true,
    component: Register,
  },
  {
    path: "/",
    name: "Landing page",
    exact: true,
    component: LandingPage,
  },
]

export default unAuthenticatedRoutes
