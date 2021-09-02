import {
  Flex,
  Box,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Input,
  Stack,
  Button,
  Heading,
  Text,
  useColorModeValue,
} from "@chakra-ui/react"
import { useState, useContext } from "react"
import { useForm } from "react-hook-form"
import { emailRegex } from "../../validation/regex"
import { useHistory } from "react-router"
import { AuthContext } from "../../store/auth-context"

import axios from "../../axios-config"

interface LoginFormValues {
  email: string
  password: string
}

const LoginPage = () => {
  const history = useHistory()

  const authContext = useContext(AuthContext)

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormValues>()

  const [error, setError] = useState(false)
  const errorMessageColor = useColorModeValue("red.500", "red.300")

  const login = async (data: LoginFormValues) => {
    try {
      const response = await axios.post("AuthManagement/login", data)
      authContext.login(
        response.data.token,
        response.data.role,
        response.data.id
      )
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
          <Heading fontSize={"4xl"}>Sign in to your account</Heading>
          <Text fontSize={"lg"} color={"gray.600"}>
            to enjoy all of our cool features ✌️
          </Text>
        </Stack>
        <Box
          rounded={"lg"}
          bg={useColorModeValue("white", "gray.700")}
          boxShadow={"lg"}
          p={8}
        >
          <form onSubmit={handleSubmit(login)}>
            <Stack spacing={4}>
              <FormControl id="email" isInvalid={errors.email !== undefined}>
                <FormLabel>Email address</FormLabel>
                <Input
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
                  type="password"
                  {...register("password", {
                    required: "Password is required",
                  })}
                  minLength={6}
                />
                <FormErrorMessage>{errors.password?.message}</FormErrorMessage>
              </FormControl>
              <Button
                bg={"blue.400"}
                color={"white"}
                _hover={{
                  bg: "blue.500",
                }}
                type="submit"
              >
                Sign in
              </Button>
              {error && (
                <Text color={errorMessageColor}>
                  Incorrect username or password!
                </Text>
              )}
            </Stack>
          </form>
        </Box>
      </Stack>
    </Flex>
  )
}

export default LoginPage
