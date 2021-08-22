import Question from "./question"

export default interface Survey {
  id: number
  name: string
  openingDate: Date
  closingDate: Date
  questions?: Question[]
}
