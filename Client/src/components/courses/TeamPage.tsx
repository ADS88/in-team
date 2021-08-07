import axios from "../../axios-config"
import { useEffect, useState } from "react"
import { RouteComponentProps } from "react-router"
import Student from "./student"
import AddStudent from "./AddStudent"
import { Flex, Stack, Text } from "@chakra-ui/layout"

const TeamPage: React.FunctionComponent<RouteComponentProps<any>> = props => {
  const id = props.match.params.id
  const [studentsInCourse, setStudentsInCourse] = useState<Student[]>([])

  const getTeam = () => {
    return axios.get(`team/${id}`)
  }

  useEffect(() => {
    getTeam().then(response => setStudentsInCourse(response.data.members))
  }, [])

  return (
    <Flex minH={"90vh"} align={"center"} justify={"center"}>
      <Stack minW={"30vw"} spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        <Text fontSize="3xl">Students</Text>
        {studentsInCourse.map(student => (
          <h4>{`${student.firstName} ${student.lastName}`}</h4>
        ))}
        <AddStudent courseId={id} />
      </Stack>
    </Flex>
  )
}

export default TeamPage
