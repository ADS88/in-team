import { useState, useReducer } from "react"
import { useForm } from "react-hook-form"
import { useHistory, useParams } from "react-router"
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

export type Action =
  | { type: "addAlpha"; payload: { newAlphaId: number } }
  | {
      type: "updateAlpha"
      payload: { alphaIdToUpdate: number; newStateId: number }
    }
  | { type: "removeAlpha"; payload: { alphaIdToRemove: number } }

const initialState = new Map<number, number>()

interface GradeIterationFormValues {
  points: number
}

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
  const [achievedStateError, setAchievedStateError] =
    useState<null | string>(null)

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<GradeIterationFormValues>()

  const gradeIteration = (data: GradeIterationFormValues) => {
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

    console.log("post req")

    axios
      .post(`team/${teamId}/achievestates/${iterationId}`, {
        points: data.points,
        achievedStates,
      })
      .then(() => history.goBack())
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
          <Heading>Grade iteration</Heading>

          <AssessAlphas dispatch={dispatch} state={state} />
          {achievedStateError && (
            <Text textColor={errorMessageColor}>{achievedStateError}</Text>
          )}
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
    </Flex>
  )
}

export default GradeTeamPage
