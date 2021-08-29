import { BadgeName } from "./badge-name"

export interface Badge {
  name: BadgeName
  id: number
  count?: number
}
