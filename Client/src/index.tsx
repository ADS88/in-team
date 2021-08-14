import React from "react"
import ReactDOM from "react-dom"
import App from "./App"

import { ChakraProvider } from "@chakra-ui/react"
import AuthContextProvider from "./store/auth-context"

ReactDOM.render(
  <React.StrictMode>
    <ChakraProvider>
      <AuthContextProvider>
        <App />
      </AuthContextProvider>
    </ChakraProvider>
  </React.StrictMode>,
  document.getElementById("root")
)
