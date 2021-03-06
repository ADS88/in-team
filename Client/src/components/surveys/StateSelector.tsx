import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { Button, Flex, Text } from "@chakra-ui/react"
import State from "../../models/state"
import Select from "../ui/Select"
import { Action } from "./CreateSurveyPage"
import { OptionsType } from "react-select"

interface StateSelectorProps {
  alphaId: number
  dispatch: (action: Action) => void
}

//Component allowing the teaching team to choose which states of an alpha to make part of a survey.
const StateSelector = (props: StateSelectorProps) => {
  const [alphaName, setAlphaName] = useState("")
  const [states, setStates] = useState<State[]>([])

  useEffect(() => {
    const getAlpha = () => {
      return axios.get(`alpha/${props.alphaId}`)
    }

    getAlpha().then(response => {
      setAlphaName(response.data.name)
      setStates(response.data.states)
    })
  }, [props.alphaId])

  const options = states.map(state => {
    return { value: state.id, label: state.name }
  })

  const onStatesChangeHandler = (
    selection: OptionsType<{
      value: number
      label: string
    }>
  ) => {
    props.dispatch({
      type: "updateAlpha",
      payload: {
        alphaIdToUpdate: props.alphaId,
        newStateIds: selection.map(s => s.value),
      },
    })
  }

  return (
    <Flex direction="column">
      <Text>{alphaName}</Text>

      <Flex direction="row" w="100%" align="center" gridGap="2">
        <div style={{ width: "100%" }}>
          <Select options={options} onChange={onStatesChangeHandler} />
        </div>
        <Button
          bg={"red.400"}
          color={"white"}
          _hover={{
            bg: "red.500",
          }}
          type="submit"
          onClick={() =>
            props.dispatch({
              type: "removeAlpha",
              payload: { alphaIdToRemove: props.alphaId },
            })
          }
        >
          x
        </Button>
      </Flex>
    </Flex>
  )
}

export default StateSelector
