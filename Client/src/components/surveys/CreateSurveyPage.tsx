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

interface CreateSurveyFormValues {
  openingDate: Date
  dueDate: Date
  name: string
}

const CreateSurveyPage = () => {
  const {
    register,
    handleSubmit,
    setError,
    control,
    formState: { errors },
  } = useForm<CreateSurveyFormValues>()

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
    >
      <Text fontSize="6xl">Create Survey</Text>

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
        <AddQuestions />
        <FormControl
          id="openingDate"
          isInvalid={errors.openingDate !== undefined}
        >
          <FormLabel>Opening date</FormLabel>
          <Controller
            control={control}
            name="openingDate"
            render={({ field }) => (
              <DatePicker
                placeholderText="e.g 04/02/2016"
                onChange={(date: Date) => field.onChange(date)}
                selected={field.value}
                maxDate={new Date()}
              />
            )}
          />
        </FormControl>
        <FormControl id="dueDate" isInvalid={errors.dueDate !== undefined}>
          <FormLabel>Due date</FormLabel>
          <Controller
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
    </Flex>
  )
}

export default CreateSurveyPage
