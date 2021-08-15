import axios from "../../axios-config"
import { useEffect, useState } from "react"
import { RouteComponentProps } from "react-router"
import Student from "../../models/student"
import AddStudent from "./AddStudent"
import { Flex, Stack, Text, Button } from "@chakra-ui/react"
import { useHistory } from "react-router-dom"
import AutoComplete from "../ui/AutoComplete"

const TeamPage: React.FunctionComponent<RouteComponentProps<any>> = props => {
  const id = props.match.params.id
  const [studentsInCourse, setStudentsInCourse] = useState<Student[]>([])
  const [teamName, setTeamName] = useState("")
  const history = useHistory()

  const deleteTeam = () => {
    axios.delete(`team/${id}`).then(() => history.goBack())
  }

  const addStudentToTeam = (student: Student) => {
    axios
      .post(`team/${id}/addstudent/${student.id}`)
      .then(() =>
        setStudentsInCourse(prevStudents => [...prevStudents, student])
      )
  }

  useEffect(() => {
    const getTeam = () => {
      return axios.get(`team/${id}`)
    }

    getTeam().then(response => {
      setStudentsInCourse(response.data.members)
      setTeamName(response.data.name)
    })
  }, [id])

  return (
    <Flex minH={"90vh"} align={"center"} justify={"center"}>
      <Stack minW={"30vw"} spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        <Text fontSize="6xl">{teamName}</Text>
        <Text fontSize="3xl">Team members</Text>
        {studentsInCourse.map(student => (
          <h4>{`${student.firstName} ${student.lastName}`}</h4>
        ))}
        <AutoComplete courseId={id} addToTeam={addStudentToTeam} />
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
