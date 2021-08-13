import LoginPage from "../components/authentication/LoginPage"
import RegisterPage from "../components/authentication/RegisterPage"
import LandingPage from "../components/landing/LandingPage"
import IRoute from "./IRoute"

const unAuthenticatedRoutes: IRoute[] = [
  {
    path: "/login",
    name: "Login page",
    exact: true,
    component: LoginPage,
  },

  {
    path: "/register",
    name: "Register page",
    exact: true,
    component: RegisterPage,
  },
  {
    path: "/",
    name: "Landing page",
    exact: true,
    component: LandingPage,
  },
]

export default unAuthenticatedRoutes
