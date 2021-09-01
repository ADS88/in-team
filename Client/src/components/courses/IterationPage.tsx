import { useEffect, useState, useReducer } from "react"
import { useForm, Controller } from "react-hook-form"
import { useParams } from "react-router"
import Team from "../../models/team"
import axios from "../../axios-config"
import {
  Select,
  Stack,
  Flex,
  useColorModeValue,
  Button,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Input,
  Heading,
  Text,
} from "@chakra-ui/react"
import AssessAlphas from "./AssessAlphas"
import Iteration from "../../models/iteration"

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

interface GradeIterationFormValues {
  team: string
  points: number
}

const IterationPage = () => {
  const { courseId, iterationId } =
    useParams<{ courseId: string; iterationId: string }>()

  const [teams, setTeams] = useState<Team[]>([])
  const [state, dispatch] = useReducer(reducer, initialState)
  const [iteration, setIteration] = useState<Iteration | null>(null)

  useEffect(() => {
    axios
      .get(`course/${courseId}`)
      .then(response => setTeams(response.data.teams))
  }, [courseId])

  useEffect(() => {
    axios.get(`course/iteration/${iterationId}`).then(response => {
      response.data.startDate = new Date(response.data.startDate)
      response.data.endDate = new Date(response.data.endDate)
      setIteration(response.data)
    })
  }, [iterationId])

  const {
    register,
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<GradeIterationFormValues>()

  const gradeIteration = (data: GradeIterationFormValues) => {
    console.log("OWO")
    console.log(data)
  }

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <form onSubmit={handleSubmit(gradeIteration)}>
        <Stack minW={"30vw"} spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
          <Heading>{iteration?.name}</Heading>
          <Text>
            {iteration?.startDate.toDateString()} -{" "}
            {iteration?.endDate.toDateString()}
          </Text>
          <FormControl>
            <FormLabel>Choose Team</FormLabel>
            <Select
              {...register(
                "team"
                // {
                //   required: "You must allocate a team",
                // }
              )}
              name="teams"
              id="teams"
            >
              {teams.map(team => (
                <option key={team.id} value={team.name}>
                  {team.name}
                </option>
              ))}
            </Select>
            {/* <FormErrorMessage>{errors.team?.message}</FormErrorMessage> */}
          </FormControl>
          <AssessAlphas dispatch={dispatch} state={state} />
          <FormControl id="points" isInvalid={errors.points !== undefined}>
            <FormLabel>Allocate points</FormLabel>
            <Input
              type="number"
              step="1"
              {...register("points", {
                required: "You must allocate points",
                min: {
                  value: 0,
                  message: "You can't allocate negative points",
                },
                max: {
                  value: 50000,
                  message: "Too many points",
                },
              })}
            />
            <FormErrorMessage>{errors.points?.message}</FormErrorMessage>
          </FormControl>
          <Button type="submit">Confirm States Achieved</Button>
        </Stack>
      </form>
    </Flex>
  )
}

export default IterationPage
