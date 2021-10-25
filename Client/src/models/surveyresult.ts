//Interface for the average of all states within an alpha, and all questions within those states
export interface AlphaResult {
  alphaName: string
  alphaId: number
  states: StateResult[]
}

//Interface for the average of answers for a state, and all questions within that state
export interface StateResult {
  stateName: string
  stateId: number
  answerSummaries: AnswerSummary[]
  average: number
}
//Interface for the average of answers for a single question
export interface AnswerSummary {
  content: string
  average: number
}
