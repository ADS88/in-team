import Team from "../courses/team"
import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { RouteComponentProps } from "react-router"
import { Button, Flex, Stack, Text } from "@chakra-ui/react"
import TeamOverview from "./TeamOverview"

const CourseDetailPage: React.FunctionComponent<RouteComponentProps<any>> =
  props => {
    const [teams, setTeams] = useState<Team[]>([])
    const [courseName, setCourseName] = useState("")

    const id = props.match.params.id
    const getCourse = () => {
      return axios.get(`course/${id}`)
    }

    useEffect(() => {
      getCourse().then(response => {
        setTeams(response.data.teams)
        setCourseName(response.data.name)
      })
    }, [])

    const addTeam = () => {
      // const team: Team =
      axios.post("team", { name: "cool name", courseId: id })
      // .then(() => setTeams(prevTeams => [...prevTeams, team]))
    }

    return (
      <Flex
        minH={"90vh"}
        align={"center"}
        justify={"center"}
        direction={"column"}
      >
        <Text fontSize="6xl">{courseName}</Text>
        <Text fontSize="xl">Teams</Text>
        <Stack spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
          {teams.map(team => (
            <TeamOverview name={team.name} id={team.id} />
          ))}
          <Button onClick={addTeam}>Add team</Button>
        </Stack>
      </Flex>
    )
  }

export default CourseDetailPage
