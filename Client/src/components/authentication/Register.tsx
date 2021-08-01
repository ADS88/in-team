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
} from "@chakra-ui/react"

import { useState } from "react"
import { useHistory } from "react-router"
import axios from "../../axios-config"

const Register = () => {
  const [firstName, setFirstName] = useState("")
  const [lastName, setlastName] = useState("")
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [error, setError] = useState(false)
  const history = useHistory()

  const register = async () => {
    try {
      const response = await axios.post("AuthManagement/Register", {
        firstName,
        lastName,
        email,
        password,
      })
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
          <Stack spacing={4}>
            <FormControl id="firstName">
              <FormLabel>First name</FormLabel>
              <Input
                minLength={2}
                type="text"
                onChange={e => setFirstName(e.target.value)}
              />
            </FormControl>
            <FormControl id="lastName">
              <FormLabel>Last name</FormLabel>
              <Input
                minLength={2}
                type="text"
                onChange={e => setlastName(e.target.value)}
              />
            </FormControl>
            <FormControl id="email">
              <FormLabel>Email address</FormLabel>
              <Input
                minLength={4}
                type="email"
                onChange={e => setEmail(e.target.value)}
              />
            </FormControl>
            <FormControl id="password">
              <FormLabel>Password</FormLabel>
              <Input
                minLength={6}
                type="password"
                onChange={e => setPassword(e.target.value)}
              />
            </FormControl>
            <Stack spacing={10}>
              <Button
                bg={"blue.400"}
                color={"white"}
                _hover={{
                  bg: "blue.500",
                }}
                onClick={register}
              >
                Create Account
              </Button>
            </Stack>
            {error && (
              <Text color={"red.500"}>Incorrect registration details!</Text>
            )}
          </Stack>
        </Box>
      </Stack>
    </Flex>
  )
}

export default Register
