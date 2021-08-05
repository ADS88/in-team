import { useContext } from "react"
import "./App.css"
import {
  Route,
  BrowserRouter as Router,
  Redirect,
  RouteComponentProps,
  RouteChildrenProps,
  Switch,
} from "react-router-dom"

import IRoute from "./routes/IRoute"

import Login from "./components/authentication/Login"
import Navbar from "./components/ui/Navbar"
import Register from "./components/authentication/Register"
import Dashboard from "./components/dashboard/Dashboard"
import { AuthContext } from "./store/auth-context"
import LandingPage from "./components/landing/LandingPage"
import CoursesPage from "./components/courses/CoursesPage"
import CourseDetailPage from "./components/courses/CourseDetailPage"
import authenticatedRoutes from "./routes/authenticated-routes"
import unAuthenticatedRoutes from "./routes/unauthenticated-routes"
function App() {
  const authContext = useContext(AuthContext)
  const routes = authContext.isLoggedIn
    ? authenticatedRoutes
    : unAuthenticatedRoutes

  return (
    <Router>
      <div className="App">
        <Navbar />
        <Switch>
          {routes.map((route: IRoute, index: number) => (
            <Route
              key={index}
              path={route.path}
              exact={route.exact}
              render={(props: RouteChildrenProps<any>) => (
                <route.component {...props} {...route.props} />
              )}
            />
          ))}
          {/* <Route path="*">
            <Redirect to="/" />
          </Route> */}
        </Switch>
      </div>
    </Router>
  )
}

export default App
