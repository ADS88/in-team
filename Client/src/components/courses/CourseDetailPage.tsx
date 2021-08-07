import Team from "../courses/team"
import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { RouteComponentProps } from "react-router"
import { Button, Flex, Stack, Text } from "@chakra-ui/react"
import TeamOverview from "./TeamOverview"
import AddTeam from "./AddTeam"

const CourseDetailPage: React.FunctionComponent<RouteComponentProps<any>> =
  props => {
    const [teams, setTeams] = useState<Team[]>([])
    const [courseName, setCourseName] = useState("")

    const addTeam = (team: Team) => {
      setTeams(prevTeams => [...prevTeams, team])
    }

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
        </Stack>
      </Flex>
    )
  }

export default CourseDetailPage
