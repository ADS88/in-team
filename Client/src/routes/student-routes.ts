import Dashboard from "../components/dashboard/Dashboard"
import IRoute from "./IRoute"

const studentRoutes: IRoute[] = [
  {
    path: "/",
    name: "Dashboard page",
    exact: true,
    component: Dashboard,
  },
]

export default studentRoutes
