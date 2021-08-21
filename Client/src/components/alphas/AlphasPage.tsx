import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { RouteComponentProps, useHistory } from "react-router"
import { Flex, Stack, Heading, useColorModeValue } from "@chakra-ui/react"
import Card from "../ui/Card"
import Alpha from "../../models/alpha"
import SingleRowForm from "../ui/SingleRowForm"

const AlphasPage: React.FunctionComponent<RouteComponentProps<any>> = props => {
  const [alphas, setAlphas] = useState<Alpha[]>([])
  const history = useHistory()

  const addAlpha = async (name: string) => {
    try {
      const response = await axios.post("alpha", { name })
      setAlphas(prevAlphas => [...prevAlphas, { name, id: response.data.id }])
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

  const allAlphas = alphas.map(({ name, id }) => (
    <div
      onClick={() => history.push(`alpha/${id}`)}
      style={{ cursor: "pointer" }}
      key={id}
    >
      <Card title={name} key={id} />
    </div>
  ))

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <Heading fontSize={"4xl"}>Alphas</Heading>
      <Stack spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        {allAlphas}
        <SingleRowForm content="alpha" addToList={addAlpha} />
      </Stack>
    </Flex>
  )
}

export default AlphasPage
