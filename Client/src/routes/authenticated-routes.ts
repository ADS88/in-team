import CourseDetailPage from "../components/courses/CourseDetailPage"
import CoursesPage from "../components/courses/CoursesPage"
import TeamPage from "../components/courses/TeamPage"
import Dashboard from "../components/dashboard/Dashboard"
import IRoute from "./IRoute"

const authenticatedRoutes: IRoute[] = [
  {
    path: "/course/:id",
    name: "Single course page",
    exact: true,
    component: CourseDetailPage,
  },
  {
    path: "/team/:id",
    name: "Team page",
    exact: true,
    component: TeamPage,
  },
  {
    path: "/courses",
    name: "Courses page",
    exact: true,
    component: CoursesPage,
  },
  {
    path: "/",
    name: "Dashboard page",
    exact: true,
    component: Dashboard,
  },
]

export default authenticatedRoutes
