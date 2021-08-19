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
import { NewLineKind } from "typescript"

const AddQuestions = () => {
  const [allAlphas, setAllAlphas] = useState<Alpha[]>([])
  const [selectedAlphaIds, setSelectedAlphaIds] = useState<number[]>([])
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
    if (newAlphaId !== null && !selectedAlphaIds.includes(newAlphaId))
      setSelectedAlphaIds(prevIds => [...prevIds, newAlphaId])
  }

  const removeAlpha = (alphaIdToRemove: number) => {
    setSelectedAlphaIds(prevIds =>
      prevIds.filter(alphaId => alphaId !== alphaIdToRemove)
    )
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
              .filter(alpha => !selectedAlphaIds.includes(alpha.id))
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
