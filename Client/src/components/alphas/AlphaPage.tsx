import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { RouteComponentProps, useHistory } from "react-router"
import { Flex, Stack, Text } from "@chakra-ui/react"
import State from "../../models/state"
import SingleRowForm from "../ui/SingleRowForm"
import Card from "../ui/Card"

const AlphaPage: React.FunctionComponent<RouteComponentProps<any>> = props => {
  const [states, setStates] = useState<State[]>([])
  const [alphaName, setAlphaName] = useState("")

  const alphaId = props.match.params.id

  useEffect(() => {
    const getAlpha = () => {
      return axios.get(`alpha/${alphaId}`)
    }

    getAlpha().then(response => {
      setStates(response.data.states)
      setAlphaName(response.data.name)
    })
  }, [alphaId])

  const addState = async (state: State) => {
    try {
      const response = await axios.post(`alpha/${alphaId}/state`, state)
      setStates(prevStates => [
        ...prevStates,
        { name: state.name, id: response.data.id },
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
      <Text fontSize="6xl">{alphaName}</Text>
      <Text fontSize="2xl">States</Text>
      <Stack spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        {states.map(state => (
          <Card title={state.name} key={state.id} />
        ))}

        <SingleRowForm addToList={addState} content="state" />
      </Stack>
    </Flex>
  )
}

export default AlphaPage
