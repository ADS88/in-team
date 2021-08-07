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
import Course from "./course"

interface AddTeamFormValues {
  name: string
}

interface AddTeamProps {
  addTeamToList: (course: Course) => void
  courseId: number
}

export default function AddTeam({ addTeamToList, courseId }: AddTeamProps) {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<AddTeamFormValues>()

  const addTeam = async (data: AddTeamFormValues) => {
    try {
      const response = await axios.post(`team`, { ...data, courseId })
      addTeamToList({ name: data.name, id: response.data.id })
    } catch (error) {
      console.log(error)
    }
  }

  return (
    <form onSubmit={handleSubmit(addTeam)}>
      <Flex direction="row" align="flex-end">
        <FormControl id="name" isInvalid={errors.name != undefined}>
          <FormLabel>Team name</FormLabel>
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
