import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { AlphaResult } from "../../models/surveyresult"
import SurveyResult from "./SurveyResult"

interface SurveyResultsProps {
  teamId: string
  iterationId: string
}

const SurveyResults = ({ teamId, iterationId }: SurveyResultsProps) => {
  const [results, setResults] = useState<AlphaResult[]>([])

  useEffect(() => {
    axios
      .get(`team/${teamId}/surveyresults/${iterationId}`)
      .then(response => setResults(response.data.alphas))
  }, [])

  return (
    <>
      {results.map(result => (
        <SurveyResult alphaResult={result} key={result.alphaId} />
      ))}
    </>
  )
}

export default SurveyResults
