import {
  FormControl,
  FormErrorMessage,
  FormLabel,
  Input,
  Stack,
  Button,
} from "@chakra-ui/react"

import { useForm } from "react-hook-form"

import axios from "../../axios-config"

interface AddIterationFormValues {
  name: String
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
    formState: { errors },
  } = useForm<AddIterationFormValues>()

  const addIteration = async () => {
    await axios.post(`/course/${courseId}/iteration`)
  }

  return (
    <form onSubmit={handleSubmit(addIteration)}>
      <Stack spacing={4}>
        <FormControl id="name" isInvalid={errors.name !== undefined}>
          <FormLabel>Namec</FormLabel>
          <Input
            {...register("name", {
              required: "You must enter a name",
            })}
          />
          {/* <FormErrorMessage>{errors.name?.message}</FormErrorMessage> */}
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