import React from "react"
import logo from "./logo.svg"
import "./App.css"
import { Route, BrowserRouter as Router } from "react-router-dom"

import { Badge } from "@chakra-ui/react"
import Login from "./components/authentication/Login"
import WithSubnavigation from "./ui/Navbar"
import Register from "./components/authentication/Register"
import Dashboard from "./components/dashboard/Dashboard"
import Landing from "./components/landing/Landing"

function App() {
  return (
    <Router>
      <div className="App">
        <WithSubnavigation />
        <Route exact path="/" component={Landing} />
        <Route path="/login" component={Login} />
        <Route path="/register" component={Register} />
        <Route path="/dashboard" component={Dashboard} />
      </div>
    </Router>
  )
}

export default App
