import Team from "../../models/team"
import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { RouteComponentProps, useHistory } from "react-router"
import {
  Button,
  Flex,
  Stack,
  Text,
  Heading,
  useColorModeValue,
} from "@chakra-ui/react"
import TeamOverview from "./TeamOverview"
import Iteration from "../../models/iteration"
import AddIteration from "./AddIteration"
import SingleRowForm from "../ui/SingleRowForm"
import Card from "../ui/Card"

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
      setTeams(prevTeams => [
        ...prevTeams,
        { name, id: response.data.id, points: 0 },
      ])
    } catch (error) {
      console.log(error)
    }
  }

  const addIterationToUI = (iteration: Iteration) => {
    setIterations(prevIterations => [...prevIterations, iteration])
  }

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
      bg={useColorModeValue("gray.50", "gray.800")}
      py="8"
    >
      <Heading fontSize="4xl">{courseName}</Heading>
      <Text fontSize="2xl">Teams</Text>
      <Stack spacing={8} mx={"auto"} maxW={"lg"} py={6} px={6}>
        {teams.map(team => (
          <TeamOverview name={team.name} id={team.id} />
        ))}

        <SingleRowForm addToList={addTeam} content="team" />

        <Text fontSize="2xl" align="center">
          Iterations
        </Text>

        {iterations.map(iteration => (
          <div
            style={{ cursor: "pointer" }}
            key={iteration.id}
            onClick={() =>
              history.push(`/course/${courseId}/iteration/${iteration.id}`)
            }
          >
            <Card title={iteration.name} />
          </div>
        ))}

        <AddIteration courseId={courseId} addIterationToUI={addIterationToUI} />

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
