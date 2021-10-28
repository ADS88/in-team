import { getQueriesForElement } from "@testing-library/react"
import { ReactElement } from "react"
import ReactDOM from "react-dom"
import LoginPage from "./LoginPage"

const render = (component: ReactElement) => {
  const root = document.createElement("div")
  ReactDOM.render(component, root)
  return getQueriesForElement(root)
}

test("The LoginPage component has instructions", () => {
  const { getByText } = render(<LoginPage />)
  getByText("Sign in to your account")
})
