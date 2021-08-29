import React, { useState } from "react"
import { Role } from "../models/role"

interface IAuthContext {
  token: string | null
  isLoggedIn: boolean
  login: (token: string, role: Role, userId: string) => void
  logout: () => void
  userId: string | null
  role: Role | null
}

const context: IAuthContext = {
  token: "",
  isLoggedIn: false,
  login: (token: string, role: Role, userId: string) => {},
  logout: () => {},
  userId: null,
  role: null,
}

export const AuthContext = React.createContext(context)

const AuthContextProvider = (props: React.PropsWithChildren<{}>) => {
  const initialToken = localStorage.getItem("token")
  const initialId = localStorage.getItem("userId")
  const initialRole: Role | null = localStorage.getItem("role") as Role
  const [token, setToken] = useState(initialToken)
  const [role, setRole] = useState<Role | null>(initialRole)
  const [userId, setUserId] = useState<string | null>(initialId)

  const userIsLogginIn = !!token
  const loginHandler = (token: string, role: Role, userId: string) => {
    localStorage.setItem("token", token)
    setToken(token)
    localStorage.setItem("role", role)
    setRole(role)
    localStorage.setItem("userId", userId)
    setUserId(userId)
  }
  const logoutHandler = () => {
    localStorage.removeItem("token")
    localStorage.removeItem("role")
    localStorage.removeItem("userId")
    setToken(null)
    setRole(null)
    setUserId(null)
  }

  const contextValue = {
    token,
    isLoggedIn: userIsLogginIn,
    login: loginHandler,
    logout: logoutHandler,
    role,
    userId,
  }

  return (
    <AuthContext.Provider value={contextValue}>
      {props.children}
    </AuthContext.Provider>
  )
}

export default AuthContextProvider
