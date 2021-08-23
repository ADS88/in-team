import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { Stack, Flex, Heading, useColorModeValue, Text } from "@chakra-ui/react"
import SurveyOverview from "./SurveyOverview"
import Survey from "../../models/survey"
import { useHistory } from "react-router"

const MySurveysPage = () => {
  const [surveys, setSurveys] = useState<Survey[]>([])
  const history = useHistory()

  useEffect(() => {
    axios.get("survey/pending").then(response => {
      response.data.forEach((survey: any) => {
        survey.openingDate = new Date(survey.openingDate)
        survey.closingDate = new Date(survey.closingDate)
      })
      setSurveys(response.data)
    })
  }, [])

  return (
    <Flex
      minH={"95vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
      p="8"
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <Heading fontSize={"4xl"}>My Surveys</Heading>
      <Stack spacing={8} mx={"auto"} maxW={"2xl"} py={12} px={6}>
        {surveys.map(survey => (
          <div
            onClick={() => history.push(`/answersurvey/${survey.id}`)}
            style={{ cursor: "pointer" }}
          >
            <SurveyOverview survey={survey} key={survey.id} />
          </div>
        ))}
        {surveys.length === 0 && <Text>All surveys completed!</Text>}
      </Stack>
    </Flex>
  )
}

export default MySurveysPage
