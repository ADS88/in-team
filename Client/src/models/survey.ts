import Question from "./question"
import Team from "./team"

export default interface Survey {
  id: number
  name: string
  openingDate: Date
  closingDate: Date
  questions?: Question[]
  teams?: Team[]
}
