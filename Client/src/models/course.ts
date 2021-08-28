import Team from "./team"

interface Course {
  id: number
  name: string
  teams?: Team[]
}

export default Course
