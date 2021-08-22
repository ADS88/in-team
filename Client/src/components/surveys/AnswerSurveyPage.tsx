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
import Question from "../../models/question"
import { LikertRating } from "../../models/likertrating"

const AnswerSurveyPage = () => {
  const { id } = useParams<{ id: string }>()
  const [survey, setSurvey] = useState<Survey | null>()
  const [answers, setAnswers] = useState(new Map<number, LikertRating>())

  useEffect(() => {
    axios.get(`survey/${id}`).then(response => {
      setSurvey(response.data)
      const defaultAnswers = new Map<number, LikertRating>()
      response.data.questions.forEach((question: Question) =>
        defaultAnswers.set(question.id, 3)
      )
      setAnswers(defaultAnswers)
    })
  }, [id])

  const updateAnswer = (questionId: number, answer: LikertRating) => {
    setAnswers(prevAnswers => {
      prevAnswers.set(questionId, answer)
      return new Map(prevAnswers)
    })
  }

  const submitAnswers = () => {
    const requestData = Array.from(answers).map(
      ([questionId, likertRating]) => {
        return { questionId, likertRating }
      }
    )
    axios.post(`survey/${id}/answer`, { answers: requestData })
  }

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
            <QuestionInput
              question={question}
              key={question.id}
              updateAnswer={updateAnswer}
            />
          ))}
          <Button
            bg={"blue.400"}
            color={"white"}
            _hover={{
              bg: "blue.500",
            }}
            onClick={submitAnswers}
          >
            Submit Answers
          </Button>
        </Stack>
      </Box>
    </Flex>
  )
}

export default AnswerSurveyPage
