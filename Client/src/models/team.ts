import Student from "./student"

interface Team {
  id: number
  name: string
  points: number
  members?: Student[]
}

export default Team
