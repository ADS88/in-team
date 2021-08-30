import { useEffect, useState } from "react"
import axios from "../../axios-config"
import {
  Stack,
  Flex,
  Heading,
  useColorModeValue,
  Icon,
  Skeleton,
} from "@chakra-ui/react"
import SurveyOverview from "./SurveyOverview"
import Survey from "../../models/survey"
import { useHistory } from "react-router"

import { GiIsland } from "react-icons/gi"

const MySurveysPage = () => {
  const [surveys, setSurveys] = useState<Survey[]>([])
  const [isLoading, setIsLoading] = useState(true)
  const history = useHistory()

  useEffect(() => {
    axios.get("survey/pending").then(response => {
      response.data.forEach((survey: any) => {
        survey.openingDate = new Date(survey.openingDate)
        survey.closingDate = new Date(survey.closingDate)
      })
      setSurveys(response.data)
      setIsLoading(false)
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
      <Skeleton isLoaded={!isLoading}>
        <Stack
          spacing={8}
          mx={"auto"}
          maxW={"2xl"}
          py={12}
          px={6}
          align="center"
        >
          {surveys.length > 0 && (
            <>
              <Heading fontSize={"4xl"}>My Surveys</Heading>

              {surveys.map(survey => (
                <div
                  onClick={() => history.push(`/answersurvey/${survey.id}`)}
                  style={{ cursor: "pointer" }}
                  key={survey.id}
                >
                  <SurveyOverview survey={survey} />
                </div>
              ))}
            </>
          )}

          {surveys.length === 0 && (
            <>
              <Heading fontSize="4xl">No Surveys Due!</Heading>
              <Icon as={GiIsland} boxSize={"80"} />
            </>
          )}
        </Stack>
      </Skeleton>
    </Flex>
  )
}

export default MySurveysPage
