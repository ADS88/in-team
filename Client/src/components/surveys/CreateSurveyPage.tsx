import { useEffect, useState } from "react"
import Alpha from "../../models/alpha"
import axios from "../../axios-config"
import {
  Button,
  Flex,
  Select,
  Stack,
  Text,
  FormControl,
  FormLabel,
  FormErrorMessage,
} from "@chakra-ui/react"
import SateSelector from "./StateSelector"
import { useForm } from "react-hook-form"
import StateSelector from "./StateSelector"

interface AddAlphaFormValues {
  alphaId: string
}

const CreateSurveyPage = () => {
  const [allAlphas, setAllAlphas] = useState<Alpha[]>([])
  const [selectedAlphaIds, setSelectedAlphaIds] = useState<number[]>([])

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm()

  useEffect(() => {
    const getAlphas = () => {
      return axios.get(`alpha`)
    }

    getAlphas().then(response => {
      setAllAlphas(response.data)
    })
  }, [])

  const addAlpha = ({ alphaId }: AddAlphaFormValues) => {
    setSelectedAlphaIds(prevIds => [...prevIds, parseInt(alphaId)])
  }

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
    >
      <Text fontSize="6xl">Alphas</Text>
      <Stack spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        {selectedAlphaIds.map(alphaId => (
          <StateSelector alphaId={alphaId} />
        ))}

        <form onSubmit={handleSubmit(addAlpha)}>
          <FormControl id="alphaId" isInvalid={errors.alphaId !== undefined}>
            <FormLabel>Select Alpha</FormLabel>
            <Select
              placeholder="Select an alpha"
              {...register("alphaId", {
                required: "You must choose an alpha",
              })}
            >
              {allAlphas
                .filter(alpha => !selectedAlphaIds.includes(alpha.id))
                .map(alpha => (
                  <option value={alpha.id} key={alpha.id}>
                    {alpha.name}
                  </option>
                ))}
            </Select>
            <FormErrorMessage>{errors.email?.message}</FormErrorMessage>
          </FormControl>

          <Button type="submit">Add alpha to survey</Button>
        </form>
        <Button>Create survey</Button>
      </Stack>
    </Flex>
  )
}

export default CreateSurveyPage
