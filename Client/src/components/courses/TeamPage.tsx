import axios from "../../axios-config"
import { useEffect, useState } from "react"
import { RouteComponentProps } from "react-router"
import Student from "../../models/student"
import { Flex, Stack, Button, useColorModeValue, Box } from "@chakra-ui/react"
import { useHistory } from "react-router-dom"
import AutoComplete from "../ui/AutoComplete"
import Team from "../../models/team"
import TeamOverview from "../ui/TeamOverview"

const TeamPage: React.FunctionComponent<RouteComponentProps<any>> = props => {
  const teamId = props.match.params.id
  const [team, setTeam] = useState<Team | null>(null)
  const history = useHistory()

  const deleteTeam = () => {
    axios.delete(`team/${teamId}`).then(() => history.goBack())
  }

  const addStudentToTeam = (student: Student) => {
    axios.post(`team/${teamId}/addstudent/${student.id}`).then(() => {
      setTeam(prevTeam => {
        if (prevTeam !== null) {
          prevTeam?.members?.push(student)
          return { ...prevTeam }
        }
        return prevTeam
      })
    })
  }

  useEffect(() => {
    const getTeam = () => {
      return axios.get(`team/${teamId}`)
    }

    getTeam().then(response => {
      setTeam(response.data)
    })
  }, [teamId])

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      bg={useColorModeValue("gray.50", "gray.800")}
      direction="column"
      marginBottom="8"
    >
      <Stack w="xl" align="center">
        {team && <TeamOverview team={team} />}

        <Flex w="xl" direction="column" gridGap="4">
          <AutoComplete teamId={teamId} addToTeam={addStudentToTeam} />
          <Button
            bg={"red.400"}
            color={"white"}
            _hover={{
              bg: "red.500",
            }}
            type="submit"
            onClick={deleteTeam}
          >
            Delete team
          </Button>
        </Flex>
      </Stack>
    </Flex>
  )
}

export default TeamPage
