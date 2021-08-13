import axios from "../../axios-config"
import {
  FormControl,
  FormErrorMessage,
  FormLabel,
  Button,
  Input,
  Flex,
} from "@chakra-ui/react"

import { useForm } from "react-hook-form"
import Course from "../../models/course"

interface AddCourseFormValues {
  name: string
}

interface AddCourseProps {
  addCourseToList: (course: Course) => void
}

export default function AddCourse({ addCourseToList }: AddCourseProps) {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<AddCourseFormValues>()

  const addCourse = async (data: AddCourseFormValues) => {
    try {
      const response = await axios.post("course", data)
      addCourseToList({ name: data.name, id: response.data.id })
    } catch (error) {
      console.log(error)
    }
  }

  return (
    <form onSubmit={handleSubmit(addCourse)}>
      <Flex direction="row" align="flex-end">
        <FormControl id="name" isInvalid={errors.name !== undefined}>
          <FormLabel>Course name</FormLabel>
          <Input
            {...register("name", {
              required: "You must enter an course name",
            })}
          />
          <FormErrorMessage>{errors.name?.message}</FormErrorMessage>
        </FormControl>
        <Button
          bg={"blue.400"}
          color={"white"}
          _hover={{
            bg: "blue.500",
          }}
          type="submit"
        >
          +
        </Button>
      </Flex>
    </form>
  )
}
