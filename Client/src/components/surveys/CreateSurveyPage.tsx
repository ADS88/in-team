import AddQuestions from "./AddQuestions"
import AddTeams from "./AddTeams"
import axios from "../../axios-config"
import {
  Button,
  Flex,
  Stack,
  Heading,
  Input,
  FormControl,
  FormLabel,
  FormErrorMessage,
  useColorModeValue,
  Box,
} from "@chakra-ui/react"
import DatePicker from "../ui/DatePicker"
import { useForm, Controller } from "react-hook-form"
import { useReducer, useState } from "react"

interface CreateSurveyFormValues {
  openingDate: Date
  dueDate: Date
  name: string
}

export type Action =
  | { type: "addAlpha"; payload: { newAlphaId: number } }
  | {
      type: "updateAlpha"
      payload: { alphaIdToUpdate: number; newStateIds: number[] }
    }
  | { type: "removeAlpha"; payload: { alphaIdToRemove: number } }

const initialState = new Map<number, number[]>()

function reducer(
  state: Map<number, number[]>,
  action: Action
): Map<number, number[]> {
  switch (action.type) {
    case "addAlpha":
      if (!state.has(action.payload.newAlphaId)) {
        state = state.set(action.payload.newAlphaId, [])
      }
      return new Map(state)
    case "updateAlpha":
      state.set(action.payload.alphaIdToUpdate, action.payload.newStateIds)
      return new Map(state)
    case "removeAlpha":
      state.delete(action.payload.alphaIdToRemove)
      return new Map(state)
    default:
      throw new Error()
  }
}

const CreateSurveyPage = () => {
  const [state, dispatch] = useReducer(reducer, initialState)
  const [teamIds, setTeamIds] = useState<number[]>([])

  const {
    register,
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<CreateSurveyFormValues>()

  const createSurvey = (data: CreateSurveyFormValues) => {
    const stateIds = Array.from(state.values()).flat()
    const request = { ...data, stateIds, teamIds }
    axios.post("survey", request)
  }

  const updateTeams = (newTeamIds: number[]) => {
    setTeamIds(newTeamIds)
  }

  return (
    <Flex
      xp
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
    >
      <Box
        rounded={"lg"}
        bg={useColorModeValue("white", "gray.700")}
        boxShadow={"lg"}
        p={8}
        m={8}
      >
        <form onSubmit={handleSubmit(createSurvey)}>
          <Heading align="center" fontSize={"4xl"}>
            Create Survey
          </Heading>
          <Stack
            spacing={8}
            mx={"auto"}
            w={["sm", "md", "lg", "xl", "2xl", "3xl"]}
            py={6}
            px={3}
          >
            <FormControl id="name" isInvalid={errors.name !== undefined}>
              <FormLabel>Survey name</FormLabel>
              <Input
                {...register("name", {
                  required: "You must enter a name",
                })}
              />
              <FormErrorMessage>{errors.name?.message}</FormErrorMessage>
            </FormControl>
            <AddQuestions dispatch={dispatch} state={state} />
            <AddTeams updateTeams={updateTeams}></AddTeams>
            <FormControl
              id="openingDate"
              isInvalid={errors.openingDate !== undefined}
            >
              <FormLabel>Opening date</FormLabel>
              <Controller
                {...register("openingDate", {
                  required: "You must enter an opening date",
                })}
                control={control}
                name="openingDate"
                render={({ field }) => (
                  <DatePicker
                    placeholderText="e.g 04/02/2016"
                    onChange={(date: Date) => field.onChange(date)}
                    selected={field.value}
                    minDate={new Date()}
                  />
                )}
              />
              <FormErrorMessage>{errors.openingDate?.message}</FormErrorMessage>
            </FormControl>
            <FormControl id="dueDate" isInvalid={errors.dueDate !== undefined}>
              <FormLabel>Due date</FormLabel>
              <Controller
                {...register("dueDate", {
                  required: "You must enter a due date",
                })}
                control={control}
                name="dueDate"
                render={({ field }) => (
                  <DatePicker
                    placeholderText="e.g 04/02/2016"
                    onChange={(date: Date) => field.onChange(date)}
                    selected={field.value}
                    minDate={new Date()}
                  />
                )}
              />
              <FormErrorMessage>{errors.dueDate?.message}</FormErrorMessage>
            </FormControl>
            <Button
              bg={"blue.400"}
              color={"white"}
              _hover={{
                bg: "blue.500",
              }}
              type="submit"
            >
              Create
            </Button>
          </Stack>
        </form>
      </Box>
    </Flex>
  )
}

export default CreateSurveyPage
