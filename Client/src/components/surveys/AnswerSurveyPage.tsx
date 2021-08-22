import { useParams } from "react-router-dom"
import { useEffect, useState } from "react"
import axios from "../../axios-config"
import {
  Flex,
  Heading,
  Stack,
  Button,
  useColorModeValue,
  Box,
} from "@chakra-ui/react"
import Survey from "../../models/survey"
import QuestionInput from "./QuestionInput"

const AnswerSurveyPage = () => {
  const { id } = useParams<{ id: string }>()
  const [survey, setSurvey] = useState<Survey | null>()

  useEffect(() => {
    axios.get(`survey/${id}`).then(response => {
      setSurvey(response.data)
    })
  }, [])

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
      p={8}
    >
      <Box
        rounded={"lg"}
        bg={useColorModeValue("white", "gray.700")}
        boxShadow={"lg"}
        p={8}
        background={useColorModeValue("white", "gray.700")}
      >
        <Heading align="center" fontSize={"4xl"}>
          {survey?.name}
        </Heading>
        <Stack spacing={16} mx={"auto"} maxW={"2xl"} py={12} px={6}>
          {survey?.questions?.map(question => (
            <QuestionInput question={question} key={question.id} />
          ))}
          <Button
            bg={"blue.400"}
            color={"white"}
            _hover={{
              bg: "blue.500",
            }}
          >
            Submit Answers
          </Button>
        </Stack>
      </Box>
    </Flex>
  )
}

export default AnswerSurveyPage
