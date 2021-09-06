import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { AlphaResult } from "../../models/surveyresult"
import SurveyResult from "./SurveyResult"
import { Stack, Heading } from "@chakra-ui/react"
import TeamsCurrentStates from "../ui/TeamCurrentStates"

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
  }, [teamId, iterationId])

  return (
    <Stack spacing="8">
      <div>
        <Heading fontSize="2xl" color="blue.500">
          Current States
        </Heading>
        <TeamsCurrentStates teamId={teamId} />
      </div>
      <div>
        <Heading fontSize="2xl" color="blue.500">
          Survey Results
        </Heading>
        <Stack spacing="8">
          {results.map(result => (
            <SurveyResult alphaResult={result} key={result.alphaId} />
          ))}
        </Stack>
      </div>
    </Stack>
  )
}

export default SurveyResults
