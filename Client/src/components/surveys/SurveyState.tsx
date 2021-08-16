import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { Button, Flex, Stack, Text } from "@chakra-ui/react"
import State from "../../models/state"
import Question from "../../models/question"
import Select from "react-select"

interface SurveyStateProps {
  alphaId: number
}

const SurveyState = ({ alphaId }: SurveyStateProps) => {
  const [alphaName, setAlphaName] = useState("")
  const [states, setStates] = useState<State[]>([])

  useEffect(() => {
    const getAlpha = () => {
      return axios.get(`alpha/${alphaId}`)
    }

    getAlpha().then(response => {
      setAlphaName(response.data.name)
      setStates(response.data.states)
    })
  }, [])

  const options = [
    { value: "chocolate", label: "Chocolate" },
    { value: "strawberry", label: "Strawberry" },
    { value: "vanilla", label: "Vanilla" },
  ]

  return (
    <>
      <h1>Alpha: {alphaName}</h1>
      <Select options={options} />
    </>
  )
}

export default SurveyState
