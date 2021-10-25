import Student from "./student"
//Interface for a team using the application, optionally has the members of that team.
interface Team {
  id: number
  name: string
  points: number
  members?: Student[]
}

export default Team
