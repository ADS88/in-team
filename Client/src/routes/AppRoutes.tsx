import { useContext } from "react"
import { AuthContext } from "../store/auth-context"
import authenticatedRoutes from "./authenticated-routes"
import unAuthenticatedRoutes from "./unauthenticated-routes"
import lecturerRoutes from "./lecturer-routes"
import studentRoutes from "./student-routes"
import IRoute from "./IRoute"
import { Route, RouteChildrenProps, Switch, Redirect } from "react-router-dom"

//Component to hold all possible routes accessible within the application
//Used to reduce complexity of the App component, and for better seperation of concerns.
const AppRoutes = () => {
  const authContext = useContext(AuthContext)
  const routes = []
  if (authContext.isLoggedIn) {
    routes.push(...authenticatedRoutes)
  } else {
    routes.push(...unAuthenticatedRoutes)
  }
  if (authContext.role === "Lecturer") {
    routes.push(...lecturerRoutes)
  } else {
    routes.push(...studentRoutes)
  }
  return (
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
      <Route path="*">
        <Redirect to="/" />
      </Route>
    </Switch>
  )
}

export default AppRoutes
