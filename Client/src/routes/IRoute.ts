//Interface for a route within the application
export default interface IRoute {
  path: string
  name: string
  exact: boolean
  component: any
  props?: any
}
