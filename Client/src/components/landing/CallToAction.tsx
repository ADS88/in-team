import { Box, Heading, Container, Text, Button, Stack } from "@chakra-ui/react"
import { useHistory } from "react-router"

const CallToAction = () => {
  const history = useHistory()

  return (
    <Container maxW={"3xl"}>
      <Stack
        as={Box}
        textAlign={"center"}
        spacing={{ base: 8, md: 14 }}
        py={{ base: 20, md: 36 }}
      >
        <Heading
          fontWeight={600}
          fontSize={{ base: "2xl", sm: "4xl", md: "6xl" }}
          lineHeight={"110%"}
        >
          <Text as={"span"} color={"pink.400"}>
            Gamify
          </Text>
          <br></br>
          your teams project experience!
        </Heading>
        <Text color={"gray.500"}>
          Improve your teams learning experience by adding game elements to your
          group projects! What are you waiting for?
        </Text>
        <Stack
          direction={"column"}
          spacing={3}
          align={"center"}
          alignSelf={"center"}
          position={"relative"}
        >
          <Button
            colorScheme={"green"}
            bg={"blue.400"}
            rounded={"full"}
            px={6}
            _hover={{
              bg: "blue.500",
            }}
            onClick={() => history.push("/register")}
          >
            Get Started
          </Button>
          <Button variant={"link"} colorScheme={"blue"} size={"sm"}>
            Learn more
          </Button>
        </Stack>
      </Stack>
    </Container>
  )
}

export default CallToAction
