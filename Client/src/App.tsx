import { BrowserRouter as Router } from "react-router-dom"
import Navbar from "./components/ui/Navbar"
import AppRoutes from "./routes/AppRoutes"

function App() {
  return (
    <Router>
      <div className="App">
        <Navbar />
        <AppRoutes />
      </div>
    </Router>
  )
}

export default App
