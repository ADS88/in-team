import axios from "../../axios-config"
import { useEffect, useState } from "react"
import { RouteComponentProps } from "react-router"
import Student from "./student"
import AddStudent from "./AddStudent"
import { Flex, Stack, Text, Button } from "@chakra-ui/react"
import { useHistory } from "react-router-dom"

const TeamPage: React.FunctionComponent<RouteComponentProps<any>> = props => {
  const id = props.match.params.id
  const [studentsInCourse, setStudentsInCourse] = useState<Student[]>([])
  const [teamName, setTeamName] = useState("")
  const history = useHistory()

  const getTeam = () => {
    return axios.get(`team/${id}`)
  }

  const deleteTeam = () => {
    axios.delete(`team/${id}`).then(() => history.goBack())
  }

  useEffect(() => {
    getTeam().then(response => {
      setStudentsInCourse(response.data.members)
      setTeamName(response.data.name)
    })
  }, [])

  return (
    <Flex minH={"90vh"} align={"center"} justify={"center"}>
      <Stack minW={"30vw"} spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        <Text fontSize="6xl">{teamName}</Text>
        <Text fontSize="3xl">Team members</Text>
        {studentsInCourse.map(student => (
          <h4>{`${student.firstName} ${student.lastName}`}</h4>
        ))}
        <AddStudent courseId={id} />
        <Button
          bg={"red.400"}
          color={"white"}
          _hover={{
            bg: "red.500",
          }}
          type="submit"
          onClick={deleteTeam}
        >
          Delete Team
        </Button>
      </Stack>
    </Flex>
  )
}

export default TeamPage
