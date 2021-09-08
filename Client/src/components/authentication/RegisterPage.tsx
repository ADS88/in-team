import {
  Flex,
  Box,
  FormControl,
  FormLabel,
  Input,
  Stack,
  Button,
  Heading,
  Text,
  useColorModeValue,
  FormErrorMessage,
  Checkbox,
} from "@chakra-ui/react"
import { useForm } from "react-hook-form"
import { useContext } from "react"
import axios from "../../axios-config"
import { AuthContext } from "../../store/auth-context"

import { emailRegex } from "../../validation/regex"

interface RegisterFormValues {
  email: string
  password: string
  firstName: string
  lastName: string
  isLecturer: boolean
  lecturerPassword: string
}

const RegisterPage = () => {
  const authContext = useContext(AuthContext)

  const {
    register,
    handleSubmit,
    setError,
    watch,
    formState: { errors },
  } = useForm<RegisterFormValues>({ defaultValues: { isLecturer: false } })

  const isLecturer = watch("isLecturer")

  const registerUser = async (data: RegisterFormValues) => {
    try {
      const response = await axios.post("AuthManagement/Register", data)
      authContext.login(
        response.data.token,
        response.data.role,
        response.data.id
      )
    } catch (error) {
      error.response?.data?.errors?.forEach((error: string) => {
        if (error.includes("Email")) {
          setError("email", {
            type: "validation",
            message: "A user with that Email already exists!",
          })
        }
        if (error.includes("password")) {
          setError("lecturerPassword", {
            type: "validation",
            message: "Wrong lecturer password",
          })
        }
      })
    }
  }

  return (
    <Flex
      minH={"95vh"}
      align={"center"}
      justify={"center"}
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <Stack minW={"30vw"} spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        <Stack align={"center"}>
          <Heading fontSize={"4xl"}>Create an account</Heading>
          <Text fontSize={"lg"} color={"gray.600"}>
            We just need a few things to get started!
          </Text>
        </Stack>
        <Box
          rounded={"lg"}
          bg={useColorModeValue("white", "gray.700")}
          boxShadow={"lg"}
          p={8}
        >
          <form onSubmit={handleSubmit(registerUser)}>
            <Stack spacing={4}>
              <FormControl
                id="firstName"
                isInvalid={errors.firstName !== undefined}
              >
                <FormLabel>First name</FormLabel>
                <Input
                  {...register("firstName", {
                    required: "You must enter a first name",
                    minLength: {
                      value: 2,
                      message: "First name must have at least 2 characters!",
                    },
                  })}
                  type="text"
                />
                <FormErrorMessage>{errors.firstName?.message}</FormErrorMessage>
              </FormControl>
              <FormControl
                id="lastName"
                isInvalid={errors.lastName !== undefined}
              >
                <FormLabel>Last name</FormLabel>
                <Input
                  {...register("lastName", {
                    required: "You must enter a last name",
                    minLength: {
                      value: 2,
                      message: "Last name must have at least 2 characters!",
                    },
                  })}
                  minLength={2}
                  type="text"
                />
                <FormErrorMessage>{errors.lastName?.message}</FormErrorMessage>
              </FormControl>
              <FormControl id="email" isInvalid={errors.email !== undefined}>
                <FormLabel>Email address</FormLabel>
                <Input
                  minLength={4}
                  type="email"
                  {...register("email", {
                    required: "You must enter an email",
                    pattern: {
                      value: emailRegex,
                      message: "Email is not valid",
                    },
                  })}
                />
                <FormErrorMessage>{errors.email?.message}</FormErrorMessage>
              </FormControl>
              <FormControl
                id="password"
                isInvalid={errors.password !== undefined}
              >
                <FormLabel>Password</FormLabel>
                <Input
                  {...register("password", {
                    required: "Password is required",
                    minLength: {
                      value: 6,
                      message: "Password must be at least 6 characters!",
                    },
                  })}
                  type="password"
                />
                <FormErrorMessage>{errors.password?.message}</FormErrorMessage>
              </FormControl>
              <FormControl
                id="isLecturer"
                isInvalid={errors.password !== undefined}
              >
                <Checkbox {...register("isLecturer")}>I am a lecturer</Checkbox>
              </FormControl>

              {isLecturer && (
                <FormControl
                  id="lecturerPassword"
                  isInvalid={errors.lecturerPassword !== undefined}
                >
                  <FormLabel>Lecturer Password</FormLabel>
                  <Input {...register("lecturerPassword")} type="password" />
                  <FormErrorMessage>
                    {errors.lecturerPassword?.message}
                  </FormErrorMessage>
                </FormControl>
              )}

              <Stack spacing={10}>
                <Button
                  type="submit"
                  bg={"blue.400"}
                  color={"white"}
                  _hover={{
                    bg: "blue.500",
                  }}
                >
                  Create Account
                </Button>
              </Stack>
            </Stack>
          </form>
        </Box>
      </Stack>
    </Flex>
  )
}

export default RegisterPage
