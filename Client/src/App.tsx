import { useContext } from "react"
import "./App.css"
import { Route, BrowserRouter as Router, Redirect } from "react-router-dom"

import Login from "./components/authentication/Login"
import Navbar from "./components/ui/Navbar"
import Register from "./components/authentication/Register"
import Dashboard from "./components/dashboard/Dashboard"
import { AuthContext } from "./store/auth-context"
import LandingPage from "./components/landing/LandingPage"
import CoursesPage from "./components/courses/CoursesPage"
import CourseDetailPage from "./components/courses/CourseDetailPage"

function App() {
  const authContext = useContext(AuthContext)

  return (
    <Router>
      <div className="App">
        <Navbar />
        {authContext.isLoggedIn && (
          <>
            <Route exact path="/" component={Dashboard} />
            <Route path="/courses" component={CoursesPage} />
            <Route
              path="/course/:id"
              component={(props: any) => <CourseDetailPage id={props.id} />}
            />
          </>
        )}
        {!authContext.isLoggedIn && (
          <>
            <Route exact path="/" component={LandingPage} />
            <Route path="/login" component={Login} />
            <Route path="/register" component={Register} />
          </>
        )}
        {/* <Route path="*">
          <Redirect to="/" />
        </Route> */}
      </div>
    </Router>
  )
}

export default App
