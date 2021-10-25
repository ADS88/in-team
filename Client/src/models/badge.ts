import { BadgeName } from "./badge-name"
//Interface for a badge, and how many times a user has received that badge
export interface Badge {
  name: BadgeName
  id: number
  count?: number
}
