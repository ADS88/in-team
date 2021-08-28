import Student from "./student"

interface Team {
  id: number
  name: string
  members?: Student[]
}

export default Team
