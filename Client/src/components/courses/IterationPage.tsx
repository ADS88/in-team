import { useEffect, useState } from "react"
import { useHistory, useParams } from "react-router"
import Team from "../../models/team"
import axios from "../../axios-config"
import {
  Stack,
  Flex,
  useColorModeValue,
  Button,
  Heading,
  Text,
  Select,
} from "@chakra-ui/react"
import Iteration from "../../models/iteration"

const IterationPage = () => {
  const { courseId, iterationId } =
    useParams<{ courseId: string; iterationId: string }>()

  const history = useHistory()

  const [teams, setTeams] = useState<Team[]>([])
  const [selectedTeamId, setSelectedTeamId] = useState<number | null>(null)
  const [iteration, setIteration] = useState<Iteration | null>(null)

  useEffect(() => {
    axios.get(`course/iteration/${iterationId}`).then(response => {
      response.data.startDate = new Date(response.data.startDate)
      response.data.endDate = new Date(response.data.endDate)
      setIteration(response.data)
    })
  }, [iterationId])

  useEffect(() => {
    axios
      .get(`course/${courseId}/pendingiteration/${iterationId}`)
      .then(response => setTeams(response.data))
  }, [courseId])

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <Stack minW={"30vw"} spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        <Heading>{iteration?.name}</Heading>
        <Text>
          {iteration?.startDate.toDateString()} -{" "}
          {iteration?.endDate.toDateString()}
        </Text>

        <Select
          name="teams"
          id="teams"
          placeholder="Select a team to assess"
          onChange={e => setSelectedTeamId(parseInt(e.target.value))}
        >
          {teams.map(team => (
            <option key={team.id} value={team.id}>
              {team.name}
            </option>
          ))}
        </Select>
        <Button
          type="submit"
          bg={"blue.400"}
          color={"white"}
          _hover={{
            bg: "blue.500",
          }}
          isDisabled={selectedTeamId === null}
          onClick={() =>
            history.push(`/team/${selectedTeamId}/gradeteam/${iterationId}`)
          }
        >
          Complete team assessment
        </Button>
      </Stack>
    </Flex>
  )
}

export default IterationPage
