import axios from "../../axios-config"
import { useState } from "react"
import {
  FormControl,
  FormErrorMessage,
  FormLabel,
  Button,
  Input,
} from "@chakra-ui/react"

import { useForm } from "react-hook-form"

interface AddCourseFormValues {
  name: string
}

export default function AddCourse() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<AddCourseFormValues>()

  const addCourse = async (data: AddCourseFormValues) => {
    axios.post("course", data)
  }

  return (
    <form onSubmit={handleSubmit(addCourse)}>
      <FormControl id="name" isInvalid={errors.name != undefined}>
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
        Add course
      </Button>
    </form>
  )
}
