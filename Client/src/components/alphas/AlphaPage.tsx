import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { RouteComponentProps } from "react-router"
import { Flex, Stack, Text } from "@chakra-ui/react"
import Card from "../ui/Card"
import Alpha from "../../models/alpha"
import AddAlpha from "./AddAlpha"

const CoursePage: React.FunctionComponent<RouteComponentProps<any>> = props => {
  const [alphas, setAlphas] = useState<Alpha[]>([])

  const addAlpha = (alpha: Alpha) => {
    setAlphas(prevAlphas => [...prevAlphas, alpha])
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

        <AddAlpha addAlphaToList={() => {}} />
      </Stack>
    </Flex>
  )
}

export default CoursePage
