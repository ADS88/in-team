import { useEffect, useState } from "react"
import Alpha from "../../models/alpha"
import axios from "../../axios-config"
import { Button, Flex, Select, FormControl, FormLabel } from "@chakra-ui/react"

import StateSelector from "./StateSelector"
import { Action } from "./CreateSurveyPage"

interface AddQuestionsProps {
  dispatch: (action: Action) => void
  state: Map<number, number[]>
}

const AddQuestions = (props: AddQuestionsProps) => {
  const [allAlphas, setAllAlphas] = useState<Alpha[]>([])
  const [newAlphaId, setNewAlphaId] = useState<number | null>(null)

  useEffect(() => {
    const getAlphas = () => {
      return axios.get(`alpha`)
    }

    getAlphas().then(response => {
      setAllAlphas(response.data)
    })
  }, [])

  const addAlpha = () => {
    if (newAlphaId !== null) {
      props.dispatch({ type: "addAlpha", payload: { newAlphaId } })
    }
  }

  return (
    <>
      <Flex direction="row" align="flex-end">
        <FormControl id="alphaId">
          <FormLabel>Select Alpha</FormLabel>
          <Select
            placeholder="Select alpha"
            onChange={e => setNewAlphaId(parseInt(e.target.value))}
          >
            {allAlphas
              .filter(alpha => !props.state.has(alpha.id))
              .map(alpha => (
                <option value={alpha.id} key={alpha.id}>
                  {alpha.name}
                </option>
              ))}
          </Select>
        </FormControl>
        <Button
          bg={"blue.400"}
          color={"white"}
          _hover={{
            bg: "blue.500",
          }}
          onClick={() => addAlpha()}
        >
          +
        </Button>
      </Flex>

      {Array.from(props.state).map(([alphaId, _]) => (
        <StateSelector
          alphaId={alphaId}
          dispatch={props.dispatch}
          key={alphaId}
        />
      ))}
    </>
  )
}

export default AddQuestions
