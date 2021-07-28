import React, { useState } from "react"

interface IAuthContext {
  token: string | null
  isLoggedIn: boolean
  login: (token: string) => void
  logout: () => void
}

const context: IAuthContext = {
  token: "",
  isLoggedIn: false,
  login: (token: string) => {},
  logout: () => {},
}

export const AuthContext = React.createContext(context)

const AuthContextProvider = (props: React.PropsWithChildren<{}>) => {
  const initialToken: string | null = localStorage.getItem("token")
  const [token, setToken] = useState(initialToken)
  const userIsLogginIn = !!token
  const loginHandler = (token: string) => {
    localStorage.setItem("token", token)
    setToken(token)
  }
  const logoutHandler = () => {
    localStorage.removeItem("token")
    setToken(null)
  }

  const contextValue = {
    token,
    isLoggedIn: userIsLogginIn,
    login: loginHandler,
    logout: logoutHandler,
  }

  return (
    <AuthContext.Provider value={contextValue}>
      {props.children}
    </AuthContext.Provider>
  )
}

export default AuthContextProvider
