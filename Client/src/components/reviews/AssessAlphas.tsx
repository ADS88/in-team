import { useEffect, useState } from "react"
import { Action } from "./GradeTeamPage"
import axios from "../../axios-config"
import { Select, Flex, Button, FormControl, FormLabel } from "@chakra-ui/react"

import Alpha from "../../models/alpha"
import SingleStateSelector from "./SingleStateSelector"

interface AssessAlphasProps {
  dispatch: (action: Action) => void
  state: Map<number, number | null>
}

//Component that allows the teaching team to choose what states a team has achieved.
const AssessAlphas = (props: AssessAlphasProps) => {
  useEffect(() => {
    axios.get("alpha").then(response => setAllAlphas(response.data))
  }, [])
  const [allAlphas, setAllAlphas] = useState<Alpha[]>([])
  const [newAlphaId, setNewAlphaId] = useState<number | null>(null)

  const addAlpha = () => {
    if (newAlphaId !== null) {
      props.dispatch({ type: "addAlpha", payload: { newAlphaId } })
    }
  }

  return (
    <>
      <Flex direction="row" align="flex-end" gridGap="2">
        <FormControl id="alphaId">
          <FormLabel>Choose Alphas</FormLabel>
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
        <SingleStateSelector
          alphaId={alphaId}
          key={alphaId}
          dispatch={props.dispatch}
        />
      ))}
    </>
  )
}

export default AssessAlphas
