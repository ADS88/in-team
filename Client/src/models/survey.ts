import Question from "./question"
import Team from "./team"
//Interface for a single survey, and optionally it's questions, and teams it has been distributed to.
export default interface Survey {
  id: number
  name: string
  openingDate: Date
  closingDate: Date
  questions?: Question[]
  teams?: Team[]
}
