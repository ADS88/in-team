import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { RouteComponentProps } from "react-router"
import { Flex, Stack, Text } from "@chakra-ui/react"
import Card from "../ui/Card"
import Alpha from "../../models/alpha"
import SingleRowForm from "../ui/SingleRowForm"

const CoursePage: React.FunctionComponent<RouteComponentProps<any>> = props => {
  const [alphas, setAlphas] = useState<Alpha[]>([])

  const addAlpha = async (alpha: Alpha) => {
    try {
      await axios.post("alpha", alpha)
      setAlphas(prevAlphas => [...prevAlphas, alpha])
    } catch (error) {
      console.log(error)
    }
  }

  useEffect(() => {
    const getAlphas = () => {
      return axios.get(`alpha`)
    }

    getAlphas().then(response => {
      setAlphas(response.data)
    })
  }, [])

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
    >
      <Text fontSize="6xl">Alphas</Text>
      <Stack spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        {alphas.map(alpha => (
          <Card title={alpha.name} key={alpha.id} />
        ))}

        <SingleRowForm content="alpha" addToList={addAlpha} />
      </Stack>
    </Flex>
  )
}

export default CoursePage
