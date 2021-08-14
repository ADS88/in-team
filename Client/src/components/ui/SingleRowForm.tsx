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

interface SingleRowFormValues {
  name: string
}

interface SingleRowFormProps {
  addToList: (content: any) => void
  content: string
}

const capitalize = (string: string) =>
  string.charAt(0).toUpperCase() + string.slice(1)

export default function SingleRowForm({
  addToList,
  content,
}: SingleRowFormProps) {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<SingleRowFormValues>()

  return (
    <form onSubmit={handleSubmit(addToList)}>
      <Flex direction="row" align="flex-end">
        <FormControl id="field" isInvalid={errors.name !== undefined}>
          <FormLabel>{capitalize(content)} name</FormLabel>
          <Input
            {...register("name", {
              required: `You must enter an ${content} name`,
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
