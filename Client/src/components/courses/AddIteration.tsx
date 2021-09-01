import {
  FormControl,
  FormErrorMessage,
  FormLabel,
  Input,
  Stack,
  Button,
} from "@chakra-ui/react"

import { useForm, Controller } from "react-hook-form"
import DatePicker from "../ui/DatePicker"
import axios from "../../axios-config"

interface AddIterationFormValues {
  name: string
  startDate: Date
  endDate: Date
}

interface AddIterationProps {
  courseId: Number
}

const AddIteration = ({ courseId }: AddIterationProps) => {
  const {
    register,
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<AddIterationFormValues>()

  const addIteration = async (data: AddIterationFormValues) => {
    await axios.post(`/course/${courseId}/iteration`, data)
  }

  return (
    <form onSubmit={handleSubmit(addIteration)}>
      <Stack spacing={4}>
        <FormControl id="name" isInvalid={errors.name !== undefined}>
          <FormLabel>Iteration name</FormLabel>
          <Input
            placeholder="e.g Sprint 1"
            {...register("name", {
              required: "You must enter a name",
            })}
          />
          <FormErrorMessage>{errors.name?.message}</FormErrorMessage>
        </FormControl>

        <FormControl id="startDate" isInvalid={errors.startDate !== undefined}>
          <FormLabel>Start Date</FormLabel>
          <Controller
            {...register("startDate", {
              required: "You must enter an start date",
            })}
            control={control}
            name="startDate"
            render={({ field }) => (
              <DatePicker
                placeholderText="e.g 04/02/2016"
                onChange={(date: Date) => field.onChange(date)}
                selected={field.value}
              />
            )}
          />
          <FormErrorMessage>{errors.startDate?.message}</FormErrorMessage>
        </FormControl>
        <FormControl id="endDate" isInvalid={errors.endDate !== undefined}>
          <FormLabel>End Date</FormLabel>
          <Controller
            {...register("endDate", {
              required: "You must enter an end date",
            })}
            control={control}
            name="endDate"
            render={({ field }) => (
              <DatePicker
                placeholderText="e.g 04/02/2016"
                onChange={(date: Date) => field.onChange(date)}
                selected={field.value}
              />
            )}
          />
          <FormErrorMessage>{errors.endDate?.message}</FormErrorMessage>
        </FormControl>

        <Button
          bg={"blue.400"}
          color={"white"}
          _hover={{
            bg: "blue.500",
          }}
          type="submit"
        >
          Add iteration
        </Button>
      </Stack>
    </form>
  )
}

export default AddIteration
