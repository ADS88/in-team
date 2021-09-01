import { useEffect, useState, useReducer } from "react"
import { useParams } from "react-router"
import Team from "../../models/team"
import axios from "../../axios-config"
import {
  Select,
  Stack,
  Flex,
  useColorModeValue,
  Button,
} from "@chakra-ui/react"
import AssessAlphas from "./AssessAlphas"
import { stat } from "fs"

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

const IterationPage = () => {
  const { courseId, iterationId } =
    useParams<{ courseId: string; iterationId: string }>()

  const [teams, setTeams] = useState<Team[]>([])
  const [state, dispatch] = useReducer(reducer, initialState)

  useEffect(() => {
    axios
      .get(`course/${courseId}`)
      .then(response => setTeams(response.data.teams))
  }, [courseId])

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <Stack minW={"30vw"} spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        <h1>
          course {courseId} iteration {iterationId}
        </h1>
        <Select name="teams" id="teams" onChange={e => console.log(e)}>
          {teams.map(team => (
            <option key={team.id} value={team.name}>
              {team.name}
            </option>
          ))}
        </Select>
        <AssessAlphas dispatch={dispatch} state={state} />
        <Button>Confirm States Achieved</Button>
      </Stack>
    </Flex>
  )
}

export default IterationPage
