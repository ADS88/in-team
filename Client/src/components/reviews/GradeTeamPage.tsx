import { useState, useReducer } from "react"
import { useHistory, useParams } from "react-router"
import axios from "../../axios-config"
import {
  Stack,
  Flex,
  useColorModeValue,
  Button,
  FormControl,
  FormLabel,
  Slider,
  SliderTrack,
  SliderThumb,
  Box,
  SliderFilledTrack,
  Heading,
  Text,
} from "@chakra-ui/react"
import AssessAlphas from "./AssessAlphas"
import SurveyResults from "./SurveyResults"

export type Action =
  | { type: "addAlpha"; payload: { newAlphaId: number } }
  | {
      type: "updateAlpha"
      payload: { alphaIdToUpdate: number; newStateId: number }
    }
  | { type: "removeAlpha"; payload: { alphaIdToRemove: number } }

const initialState = new Map<number, number>()

function reducer(
  state: Map<number, number | null>,
  action: Action
): Map<number, number | null> {
  switch (action.type) {
    case "addAlpha":
      if (!state.has(action.payload.newAlphaId)) {
        state = state.set(action.payload.newAlphaId, null)
      }
      return new Map(state)
    case "updateAlpha":
      state.set(action.payload.alphaIdToUpdate, action.payload.newStateId)
      return new Map(state)
    case "removeAlpha":
      state.delete(action.payload.alphaIdToRemove)
      return new Map(state)
    default:
      throw new Error()
  }
}

const GradeTeamPage = () => {
  const { teamId, iterationId } =
    useParams<{ teamId: string; iterationId: string }>()

  const history = useHistory()

  const errorMessageColor = useColorModeValue("red.500", "red.300")
  const [state, dispatch] = useReducer(reducer, initialState)
  const [pointsGiven, setPointsGiven] = useState(50)
  const [achievedStateError, setAchievedStateError] =
    useState<null | string>(null)

  const gradeIteration = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    if (state.size === 0) {
      setAchievedStateError("You must add at least one alpha")
      return
    }
    if (Array.from(state.values()).some(stateId => stateId === null)) {
      setAchievedStateError("All selected alphas must have a state value")
      return
    }
    const achievedStates = Array.from(state).map(([alphaId, stateId]) => {
      return { alphaId, stateId }
    })

    axios
      .post(`team/${teamId}/achievestates/${iterationId}`, {
        points: pointsGiven,
        achievedStates,
      })
      .then(() => history.goBack())
  }

  return (
    <Flex
      p="8"
      minH={"90vh"}
      align={"center"}
      justify="center"
      gridGap={{ sm: "5", lg: "20" }}
      direction={{ sm: "column-reverse", lg: "row" }}
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <form onSubmit={gradeIteration}>
        <Stack minW={"30vw"} spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
          <Heading>Grade iteration</Heading>

          <AssessAlphas dispatch={dispatch} state={state} />
          {achievedStateError && (
            <Text textColor={errorMessageColor}>{achievedStateError}</Text>
          )}
          <FormControl id="points">
            <FormLabel>Allocate points</FormLabel>
            <Text
              p="1"
              textAlign="center"
              color={useColorModeValue("gray.600", "gray.300")}
            >
              {pointsGiven} points
            </Text>
            <Slider
              defaultValue={pointsGiven}
              min={0}
              max={100}
              step={5}
              onChange={(value: number) => {
                setPointsGiven(value)
              }}
            >
              <SliderTrack bg="pink.100">
                <Box position="relative" right={10} />
                <SliderFilledTrack bg="pink.300" />
              </SliderTrack>
              <SliderThumb boxSize={6} />
            </Slider>
          </FormControl>
          <Button
            type="submit"
            bg={"blue.400"}
            color={"white"}
            _hover={{
              bg: "blue.500",
            }}
          >
            Confirm States Achieved
          </Button>
        </Stack>
      </form>
      <SurveyResults teamId={teamId} iterationId={iterationId} />
    </Flex>
  )
}

export default GradeTeamPage
