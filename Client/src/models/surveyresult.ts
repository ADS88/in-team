export interface AlphaResult {
  alphaName: string
  alphaId: number
  states: StateResult[]
}

export interface StateResult {
  stateName: string
  stateId: number
  answerSummaries: AnswerSummary[]
  average: number
}

export interface AnswerSummary {
  content: string
  average: number
}
