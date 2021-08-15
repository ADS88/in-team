import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { RouteComponentProps } from "react-router"
import { Flex, Stack, Text } from "@chakra-ui/react"
import Question from "../../models/question"
import SingleRowForm from "../ui/SingleRowForm"
import Card from "../ui/Card"

const StatePage: React.FunctionComponent<RouteComponentProps<any>> = props => {
  const [questions, setQuestions] = useState<Question[]>([])
  const [stateName, setStateName] = useState("")

  const stateId = props.match.params.id

  useEffect(() => {
    const getState = () => {
      return axios.get(`alpha/state/${stateId}`)
    }

    getState().then(response => {
      setQuestions(response.data.questions)
      setStateName(response.data.name)
    })
  }, [stateId])

  const addQuestion = async (content: string) => {
    try {
      const response = await axios.post(`alpha/state/${stateId}/question`, {
        content,
      })
      setQuestions(prevQuestions => [
        ...prevQuestions,
        { content, id: response.data.id },
      ])
    } catch (error) {
      console.log(error)
    }
  }

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
    >
      <Text fontSize="6xl">{stateName}</Text>
      <Text fontSize="2xl">Questions</Text>
      <Stack spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        {questions.map(question => (
          <Card title={question.content} key={question.id} />
        ))}

        <SingleRowForm addToList={addQuestion} content="question" />
      </Stack>
    </Flex>
  )
}

export default StatePage
