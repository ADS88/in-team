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
} from "@chakra-ui/react"
import { useForm } from "react-hook-form"
import { useState, useContext } from "react"
import { useHistory } from "react-router"
import axios from "../../axios-config"
import { AuthContext } from "../../store/auth-context"

import { emailRegex } from "../../validation/regex"

interface RegisterFormValues {
  email: string
  password: string
  firstName: string
  lastName: string
}

const Register = () => {
  const [error, setError] = useState(false)
  const history = useHistory()
  const authContext = useContext(AuthContext)

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterFormValues>()

  const registerUser = async (data: RegisterFormValues) => {
    try {
      const response = await axios.post("AuthManagement/Register", data)
      authContext.login(response.data.token)
      history.push("/")
    } catch (error) {
      setError(true)
    }
  }

  return (
    <Flex
      minH={"90vh"}
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
                  minLength={6}
                  type="password"
                />
                <FormErrorMessage>{errors.password?.message}</FormErrorMessage>
              </FormControl>
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
              {error && (
                <Text color={"red.500"}>
                  Email already in use, please use another!
                </Text>
              )}
            </Stack>
          </form>
        </Box>
      </Stack>
    </Flex>
  )
}

export default Register
