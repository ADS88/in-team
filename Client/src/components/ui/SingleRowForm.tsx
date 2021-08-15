import {
  FormControl,
  FormErrorMessage,
  FormLabel,
  Button,
  Input,
  Flex,
} from "@chakra-ui/react"
import { useForm } from "react-hook-form"

interface SingleRowFormValues {
  input: string
}

interface SingleRowFormProps {
  addToList: (input: string) => void
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
    <form onSubmit={handleSubmit(form => addToList(form.input))}>
      <Flex direction="row" align="flex-end">
        <FormControl id="field" isInvalid={errors.input !== undefined}>
          <FormLabel>{capitalize(content)} name</FormLabel>
          <Input
            {...register("input", {
              required: `You must enter an ${content} name`,
            })}
          />
          <FormErrorMessage>{errors.input?.message}</FormErrorMessage>
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
