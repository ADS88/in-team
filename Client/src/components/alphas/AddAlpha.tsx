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

interface AddAlphaFormValues {
  name: string
}

interface AddAlphaProps {
  addAlphaToList: (course: Course) => void
}

export default function AddAlpha({ addAlphaToList }: AddAlphaProps) {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<AddAlphaFormValues>()

  const addCourse = async (data: AddAlphaFormValues) => {
    try {
      const response = await axios.post("alpha", data)
      addAlphaToList({ name: data.name, id: response.data.id })
    } catch (error) {
      console.log(error)
    }
  }

  return (
    <form onSubmit={handleSubmit(addCourse)}>
      <Flex direction="row" align="flex-end">
        <FormControl id="name" isInvalid={errors.name !== undefined}>
          <FormLabel>Alpha name</FormLabel>
          <Input
            {...register("name", {
              required: "You must enter an alpha name",
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
