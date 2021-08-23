import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { Stack, Flex, Heading, useColorModeValue } from "@chakra-ui/react"
import SurveyOverview from "./SurveyOverview"
import Survey from "../../models/survey"

const MySurveysPage = () => {
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
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <Heading fontSize={"4xl"}>My Surveys</Heading>
      <Stack spacing={8} mx={"auto"} maxW={"2xl"} py={12} px={6}>
        {surveys.map(survey => (
          <SurveyOverview survey={survey} key={survey.id} />
        ))}
      </Stack>
    </Flex>
  )
}

export default MySurveysPage
