import Team from "../../models/team"
import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { RouteComponentProps, useHistory } from "react-router"
import { Button, Flex, Stack, Text } from "@chakra-ui/react"
import TeamOverview from "./TeamOverview"
import Iteration from "../../models/iteration"
import AddIteration from "./AddIteration"
import SingleRowForm from "../ui/SingleRowForm"

const CoursePage: React.FunctionComponent<RouteComponentProps<any>> = props => {
  const [teams, setTeams] = useState<Team[]>([])
  const [iterations, setIterations] = useState<Iteration[]>([])
  const [courseName, setCourseName] = useState("")
  const history = useHistory()

  const courseId = props.match.params.id

  const deleteCourse = () => {
    axios.delete(`course/${courseId}`).then(() => history.goBack())
  }

  useEffect(() => {
    const getCourse = () => {
      return axios.get(`course/${courseId}`)
    }

    getCourse().then(response => {
      setTeams(response.data.teams)
      setCourseName(response.data.name)
      setIterations(response.data.iterations)
    })
  }, [courseId])

  const addTeam = async (name: string) => {
    try {
      const response = await axios.post(`team`, { name, courseId })
      setTeams(prevTeams => [...prevTeams, { name, id: response.data.id }])
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
      <Text fontSize="6xl">{courseName}</Text>
      <Text fontSize="2xl">Teams</Text>
      <Stack spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        {teams.map(team => (
          <TeamOverview name={team.name} id={team.id} />
        ))}

        <SingleRowForm addToList={addTeam} content="team" />

        <Text fontSize="2xl">Iterations</Text>

        {iterations.map(iteration => (
          <TeamOverview name={iteration.name} id={iteration.id} />
        ))}

        <AddIteration courseId={courseId} />

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
