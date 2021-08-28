import React, { useState } from "react"
import { Role } from "../models/role"

interface IAuthContext {
  token: string | null
  isLoggedIn: boolean
  login: (token: string, role: Role) => void
  logout: () => void
  role: Role | null
}

const context: IAuthContext = {
  token: "",
  isLoggedIn: false,
  login: (token: string, role: Role) => {},
  logout: () => {},
  role: null,
}

export const AuthContext = React.createContext(context)

const AuthContextProvider = (props: React.PropsWithChildren<{}>) => {
  const initialToken = localStorage.getItem("token")
  const initialRole: Role | null = localStorage.getItem("role") as Role
  const [token, setToken] = useState(initialToken)
  const [role, setRole] = useState<Role | null>(initialRole)

  const userIsLogginIn = !!token
  const loginHandler = (token: string, role: Role) => {
    localStorage.setItem("token", token)
    setToken(token)
    localStorage.setItem("role", role)
    setRole(role)
  }
  const logoutHandler = () => {
    localStorage.removeItem("token")
    localStorage.removeItem("role")
    setToken(null)
    setRole(null)
  }

  const contextValue = {
    token,
    isLoggedIn: userIsLogginIn,
    login: loginHandler,
    logout: logoutHandler,
    role,
  }

  return (
    <AuthContext.Provider value={contextValue}>
      {props.children}
    </AuthContext.Provider>
  )
}

export default AuthContextProvider
