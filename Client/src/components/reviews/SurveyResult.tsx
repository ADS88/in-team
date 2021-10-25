import { AlphaResult } from "../../models/surveyresult"
import { Heading, Stack, Text } from "@chakra-ui/react"

export interface SurveyResultProps {
  alphaResult: AlphaResult
}

const toDecimalPlaces = (num: number, numDecimalPlaces: number) =>
  (Math.round(num * 100) / 100).toFixed(numDecimalPlaces)

//Shows the survey result for a single alpha
const SurveyResult = ({ alphaResult }: SurveyResultProps) => {
  return (
    <Stack>
      <Heading fontSize="xl" color="pink.500">
        {alphaResult.alphaName}
      </Heading>
      <Stack spacing="10">
        {alphaResult.states.map(state => (
          <Stack>
            <Text fontSize="md">
              {state.stateName}: {toDecimalPlaces(state.average, 1)} / 5
            </Text>
            {state.answerSummaries.map(answer => (
              <Text fontSize="sm">
                {answer.content}: {toDecimalPlaces(answer.average, 1)} / 5
              </Text>
            ))}
          </Stack>
        ))}
      </Stack>
    </Stack>
  )
}

export default SurveyResult
