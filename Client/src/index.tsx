import React from "react"
import ReactDOM from "react-dom"
import App from "./App"

import { ChakraProvider, ColorModeScript } from "@chakra-ui/react"
import theme from "./theme"
import AuthContextProvider from "./store/auth-context"

ReactDOM.render(
  <React.StrictMode>
    <ChakraProvider theme={theme}>
      <AuthContextProvider>
        <ColorModeScript initialColorMode={theme.config.initialColorMode} />
        <App />
      </AuthContextProvider>
    </ChakraProvider>
  </React.StrictMode>,
  document.getElementById("root")
)
