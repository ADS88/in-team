import axios from "../../axios-config"
import { useEffect, useState } from "react"
import { RouteComponentProps } from "react-router"
import Student from "../../models/student"
import {
  Flex,
  Stack,
  Text,
  Button,
  Heading,
  useColorModeValue,
} from "@chakra-ui/react"
import { useHistory } from "react-router-dom"
import AutoComplete from "../ui/AutoComplete"
import Card from "../ui/Card"

const TeamPage: React.FunctionComponent<RouteComponentProps<any>> = props => {
  const teamId = props.match.params.id
  const [studentsInCourse, setStudentsInCourse] = useState<Student[]>([])
  const [teamName, setTeamName] = useState("")
  const history = useHistory()

  const deleteTeam = () => {
    axios.delete(`team/${teamId}`).then(() => history.goBack())
  }

  const addStudentToTeam = (student: Student) => {
    axios
      .post(`team/${teamId}/addstudent/${student.id}`)
      .then(() =>
        setStudentsInCourse(prevStudents => [...prevStudents, student])
      )
  }

  useEffect(() => {
    const getTeam = () => {
      return axios.get(`team/${teamId}`)
    }

    getTeam().then(response => {
      setStudentsInCourse(response.data.members)
      setTeamName(response.data.name)
    })
  }, [teamId])

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      bg={useColorModeValue("gray.50", "gray.800")}
      direction="column"
    >
      <Heading fontSize="4xl">{teamName}</Heading>
      <Text fontSize="2xl">Members</Text>
      <Stack minW={"30vw"} spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        {studentsInCourse.map(student => (
          <Card title={`${student.firstName} ${student.lastName}`} />
        ))}
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
      </Stack>
    </Flex>
  )
}

export default TeamPage
