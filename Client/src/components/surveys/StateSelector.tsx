import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { Button, Flex, Text } from "@chakra-ui/react"
import State from "../../models/state"
import Select from "react-select"

interface StateSelectorProps {
  alphaId: number
  removeAlpha: () => void
}

const StateSelector = ({ alphaId, removeAlpha }: StateSelectorProps) => {
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
  }, [alphaId])

  const options = states.map(state => {
    return { value: state.id, label: state.name }
  })

  return (
    <Flex direction="column">
      <Text>{alphaName}</Text>

      <Flex direction="row" w="100%" alignContent="center">
        <div style={{ width: "100%" }}>
          <Select isMulti options={options} style={{ width: 500 }} />
        </div>
        <Button
          bg={"red.400"}
          color={"white"}
          _hover={{
            bg: "red.500",
          }}
          type="submit"
          onClick={removeAlpha}
        >
          x
        </Button>
      </Flex>
    </Flex>
  )
}

export default StateSelector
