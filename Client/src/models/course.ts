import Team from "./team"
//Interface for a course, and optionally the teams inside it
interface Course {
  id: number
  name: string
  teams?: Team[]
}

export default Course
