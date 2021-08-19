import AddQuestions from "./AddQuestions"
import {
  Button,
  Flex,
  Stack,
  Text,
  Input,
  FormControl,
  FormLabel,
  FormErrorMessage,
} from "@chakra-ui/react"
import DatePicker from "../ui/DatePicker"

import { useForm, Controller } from "react-hook-form"
import { useReducer } from "react"

interface CreateSurveyFormValues {
  openingDate: Date
  dueDate: Date
  name: string
}

export interface QuestionSelection {
  alphaId: number
  stateIds: number[]
}

export type Action =
  | { type: "addAlpha"; payload: { newAlphaId: number } }
  | {
      type: "updateAlpha"
      payload: { alphaIdToUpdate: number; newStateIds: number[] }
    }
  | { type: "removeAlpha"; payload: { alphaIdToRemove: number } }

const initialState: QuestionSelection[] = []

function reducer(state: QuestionSelection[], action: Action) {
  switch (action.type) {
    case "addAlpha":
      if (
        state.some(
          questionSelection =>
            questionSelection.alphaId === action.payload.newAlphaId
        )
      ) {
        return state
      }
      return [...state, { alphaId: action.payload.newAlphaId, stateIds: [] }]
    case "updateAlpha":
      state = state.filter(
        alpha => alpha.alphaId !== action.payload.alphaIdToUpdate
      )
      state.push({
        alphaId: action.payload.alphaIdToUpdate,
        stateIds: action.payload.newStateIds,
      })
      return state
    case "removeAlpha":
      return state.filter(
        alpha => alpha.alphaId !== action.payload.alphaIdToRemove
      )
    default:
      throw new Error()
  }
}

const CreateSurveyPage = () => {
  const [state, dispatch] = useReducer(reducer, initialState)

  const {
    register,
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<CreateSurveyFormValues>()

  const createSurvey = (data: CreateSurveyFormValues) => {
    console.log(data)
  }

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
    >
      <Text fontSize="6xl">Create Survey</Text>

      <form onSubmit={handleSubmit(createSurvey)}>
        <Stack
          spacing={8}
          mx={"auto"}
          w={["sm", "md", "lg", "xl", "2xl", "3xl"]}
          py={12}
          px={6}
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
            Create Survey
          </Button>
        </Stack>
      </form>
    </Flex>
  )
}

export default CreateSurveyPage
