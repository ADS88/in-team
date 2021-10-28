import { getQueriesForElement } from "@testing-library/react"
import { ReactElement } from "react"
import ReactDOM from "react-dom"
import RegisterPage from "./RegisterPage"

const render = (component: ReactElement) => {
  const root = document.createElement("div")
  ReactDOM.render(component, root)
  return getQueriesForElement(root)
}

test("The LoginPage component has instructions", () => {
  const { getByText } = render(<RegisterPage />)
  getByText("Create an account")
})
