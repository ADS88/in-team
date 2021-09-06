import { AlphaResult } from "../../models/surveyresult"
import { Heading, Stack, Text } from "@chakra-ui/react"

export interface SurveyResultProps {
  alphaResult: AlphaResult
}

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
              {state.stateName}: {state.average} / 5
            </Text>
            {state.answerSummaries.map(answer => (
              <Text fontSize="sm">
                {answer.content}: {answer.average} / 5
              </Text>
            ))}
          </Stack>
        ))}
      </Stack>
    </Stack>
  )
}

export default SurveyResult
