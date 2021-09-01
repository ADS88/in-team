import { useEffect, useState } from "react"
import State from "../../models/state"
import axios from "../../axios-config"
import { Select, Flex, Text, Button } from "@chakra-ui/react"
import { Action } from "./IterationPage"

interface SingleStateSelectorProps {
  alphaId: number
  dispatch: (action: Action) => void
}

const SingleStateSelector = (props: SingleStateSelectorProps) => {
  const [states, setStates] = useState<State[]>([])
  const [alphaName, setAlphaName] = useState("")

  useEffect(() => {
    axios.get(`alpha/${props.alphaId}`).then(response => {
      setAlphaName(response.data.name)
      setStates(response.data.states)
    })
  }, [props.alphaId])

  const options = states.map(state => {
    return (
      <option key={state.id} value={state.id}>
        {state.name}
      </option>
    )
  })

  const onStatesChangeHandler = (e: any) => {
    props.dispatch({
      type: "updateAlpha",
      payload: {
        alphaIdToUpdate: props.alphaId,
        newStateId: parseInt(e.target.value),
      },
    })
  }

  return (
    <Flex direction="column">
      <Text>{alphaName}</Text>

      <Flex direction="row" w="100%" align="center" gridGap="2">
        <div style={{ width: "100%" }}>
          <Select onChange={onStatesChangeHandler} placeholder="Select state">
            {options}
          </Select>
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
export default SingleStateSelector
