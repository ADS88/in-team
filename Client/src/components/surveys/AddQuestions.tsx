import { useEffect, useState } from "react"
import Alpha from "../../models/alpha"
import axios from "../../axios-config"
import {
  Button,
  Flex,
  Select,
  FormControl,
  FormLabel,
  FormErrorMessage,
} from "@chakra-ui/react"
import { useForm } from "react-hook-form"
import StateSelector from "./StateSelector"

interface AddAlphaFormValues {
  alphaId: string
}

const AddQuestions = () => {
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

  const removeAlpha = (alphaIdToRemove: number) => {
    setSelectedAlphaIds(prevIds =>
      prevIds.filter(alphaId => alphaId !== alphaIdToRemove)
    )
  }

  return (
    <>
      <form onSubmit={handleSubmit(addAlpha)}>
        <Flex direction="row" align="flex-end">
          <FormControl id="alphaId" isInvalid={errors.alphaId !== undefined}>
            <FormLabel>Select Alpha</FormLabel>
            <Select
              placeholder="Select alpha"
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
          <Button
            bg={"blue.400"}
            color={"white"}
            _hover={{
              bg: "blue.500",
            }}
            type="submit"
          >
            +
          </Button>
        </Flex>
      </form>
      {selectedAlphaIds.map(alphaId => (
        <StateSelector
          alphaId={alphaId}
          removeAlpha={() => removeAlpha(alphaId)}
        />
      ))}
    </>
  )
}

export default AddQuestions
