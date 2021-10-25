import Dashboard from "../components/dashboard/Dashboard"
import IRoute from "./IRoute"

//Routes within the application that only students can access
const studentRoutes: IRoute[] = [
  {
    path: "/",
    name: "Dashboard page",
    exact: true,
    component: Dashboard,
  },
]

export default studentRoutes
