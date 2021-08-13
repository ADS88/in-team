import Team from "../../models/team"
import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { RouteComponentProps, useHistory } from "react-router"
import { Button, Flex, Stack, Text } from "@chakra-ui/react"
import TeamOverview from "./TeamOverview"
import AddTeam from "./AddTeam"
import Iteration from "../../models/iteration"
import AddIteration from "./AddIteration"

const CoursePage: React.FunctionComponent<RouteComponentProps<any>> = props => {
  const [teams, setTeams] = useState<Team[]>([])
  const [iterations, setIterations] = useState<Iteration[]>([])
  const [courseName, setCourseName] = useState("")
  const history = useHistory()

  const addTeam = (team: Team) => {
    setTeams(prevTeams => [...prevTeams, team])
  }

  const id = props.match.params.id

  const deleteCourse = () => {
    axios.delete(`course/${id}`).then(() => history.goBack())
  }

  useEffect(() => {
    const getCourse = () => {
      return axios.get(`course/${id}`)
    }

    getCourse().then(response => {
      setTeams(response.data.teams)
      setCourseName(response.data.name)
      setIterations(response.data.iterations)
    })
  }, [id])

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
    >
      <Text fontSize="6xl">{courseName}</Text>
      <Text fontSize="2xl">Teams</Text>
      <Stack spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        {teams.map(team => (
          <TeamOverview name={team.name} id={team.id} />
        ))}
        <AddTeam addTeamToList={addTeam} courseId={id} />

        <Text fontSize="2xl">Iterations</Text>

        {iterations.map(iteration => (
          <TeamOverview name={iteration.name} id={iteration.id} />
        ))}

        <AddIteration courseId={id} />

        <Button
          bg={"red.400"}
          color={"white"}
          _hover={{
            bg: "red.500",
          }}
          type="submit"
          onClick={deleteCourse}
        >
          Delete Course
        </Button>
      </Stack>
    </Flex>
  )
}

export default CoursePage
