import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { Flex, Text } from "@chakra-ui/react"
import State from "../../models/state"
import Select from "react-select"

interface StateSelectorProps {
  alphaId: number
}

const StateSelector = ({ alphaId }: StateSelectorProps) => {
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

  const options = states.map(state => {
    return { value: state.id, label: state.name }
  })

  return (
    <Flex direction="row" alignItems="center">
      <Text px="4">{alphaName}</Text>
      <Select isMulti options={options} className="basic-multi-select" />
    </Flex>
  )
}

export default StateSelector
