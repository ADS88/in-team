import { useHistory, useParams } from "react-router-dom"
import { useEffect, useState } from "react"
import axios from "../../axios-config"
import {
  Flex,
  Heading,
  Stack,
  Button,
  useColorModeValue,
  Box,
  Text,
} from "@chakra-ui/react"
import Survey from "../../models/survey"
import QuestionInput from "./QuestionInput"
import Question from "../../models/question"
import { LikertRating } from "../../models/likertrating"
import { Badge as IBadge } from "../../models/badge"
import BadgeGift from "./BadgeGift"
import Student from "../../models/student"

//Page shown to a student when answering a survey.
//On this page they answer a series of questions using a likert scale, and can award badges to teammates.
const AnswerSurveyPage = () => {
  const { id } = useParams<{ id: string }>()
  const [survey, setSurvey] = useState<Survey | null>()
  const [answers, setAnswers] = useState(new Map<number, LikertRating>())
  const [badgeGifts, setBadgeGifts] = useState(new Map<number, string>())
  const [badges, setBadges] = useState<IBadge[]>([])
  const [members, setMembers] = useState<Student[]>([])
  const [errorMessage, setErrorMessage] = useState("")
  const errorMessageColor = useColorModeValue("red.500", "red.300")
  const history = useHistory()

  useEffect(() => {
    axios.get(`survey/${id}`).then(response => {
      setSurvey(response.data)
      const defaultAnswers = new Map<number, LikertRating>()
      response.data.questions.forEach((question: Question) =>
        defaultAnswers.set(question.id, 3)
      )
      setAnswers(defaultAnswers)
    })
    axios.get("survey/badges").then(response => {
      const defaultBadgeGifts = new Map<number, string>()
      response.data.forEach((badge: IBadge) =>
        defaultBadgeGifts.set(badge.id, "")
      )
      setBadges(response.data)
      setBadgeGifts(defaultBadgeGifts)
    })
    axios
      .get(`survey/${id}/members`)
      .then(response => setMembers(response.data))
  }, [id])

  const updateAnswer = (questionId: number, answer: LikertRating) => {
    setAnswers(prevAnswers => {
      prevAnswers.set(questionId, answer)
      return new Map(prevAnswers)
    })
  }

  const updateBadgeGift = (badgeId: number, studentId: string) => {
    setBadgeGifts(prevBadgeGifts => {
      prevBadgeGifts.set(badgeId, studentId)
      return new Map(prevBadgeGifts)
    })
  }

  const submitAnswers = () => {
    if (Array.from(badgeGifts.values()).includes("")) {
      setErrorMessage("You must choose a member for each badge")
      return
    }

    const badgeGiftsData = Array.from(badgeGifts).map(([badgeId, userId]) => {
      return { badgeId, userId }
    })

    const answersData = Array.from(answers).map(
      ([questionId, likertRating]) => {
        return { questionId, likertRating }
      }
    )

    axios
      .post(`survey/${id}/answer`, {
        answers: answersData,
        badgeGifts: badgeGiftsData,
      })
      .then(() => history.push("/"))
  }

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
      bg={useColorModeValue("gray.50", "gray.800")}
      p={8}
    >
      <Box
        rounded={"lg"}
        w={{ sm: "md", lg: "2xl", xl: "4xl" }}
        boxShadow={"lg"}
        p={8}
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

          {badges.map(badge => (
            <BadgeGift
              badge={badge}
              members={members}
              key={badge.id}
              updateBadgeGift={updateBadgeGift}
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
          {errorMessage !== "" && (
            <Text alignSelf="center" color={errorMessageColor}>
              {errorMessage}
            </Text>
          )}
        </Stack>
      </Box>
    </Flex>
  )
}

export default AnswerSurveyPage
