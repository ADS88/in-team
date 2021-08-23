import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { Button } from "@chakra-ui/react"
import { Link as RouterLink } from "react-router-dom"
import { Stack, Flex, Heading } from "@chakra-ui/react"
import SurveyOverview from "./SurveyOverview"
import Survey from "../../models/survey"

const SurveyPage = () => {
  const [surveys, setSurveys] = useState<Survey[]>([])

  useEffect(() => {
    axios.get("survey").then(response => {
      response.data.forEach((survey: any) => {
        survey.openingDate = new Date(survey.openingDate)
        survey.closingDate = new Date(survey.closingDate)
      })
      setSurveys(response.data)
    })
  }, [])

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
      p="8"
    >
      <Heading fontSize={"4xl"}>Surveys</Heading>
      <Stack spacing={8} mx={"auto"} maxW={"2xl"} py={12} px={6}>
        {surveys.map(survey => (
          <SurveyOverview survey={survey} key={survey.id} />
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
          Create Survey
        </Button>
      </Stack>
    </Flex>
  )
}

export default SurveyPage
