import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { Button } from "@chakra-ui/react"
import { Link as RouterLink } from "react-router-dom"
import { Stack, Flex, Text } from "@chakra-ui/react"

const SurveyPage = () => {
  const [surveys, setSurveys] = useState<any[]>([])

  useEffect(() => {
    axios.get("survey").then(response => setSurveys(response.data))
  }, [])

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
    >
      <Text fontSize="6xl">Surveys</Text>
      <Stack spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        {surveys.map(survey => (
          <h1>{survey.name}</h1>
        ))}

        <Button
          bg={"blue.400"}
          color={"white"}
          _hover={{
            bg: "blue.500",
          }}
          as={RouterLink}
          to="createsurvey"
        >
          Add Survey
        </Button>
      </Stack>
    </Flex>
  )
}

export default SurveyPage
