import axios from "../../axios-config"
import { useEffect, useState } from "react"
import { RouteComponentProps } from "react-router"
import Student from "./student"

interface TeamPageProps {
  name: string
}

const TeamPage: React.FunctionComponent<RouteComponentProps<any>> = props => {
  const id = props.match.params.id
  const [students, setStudents] = useState<Student[]>([])

  const getTeam = () => {
    return axios.get(`team/${id}`)
  }

  useEffect(() => {
    getTeam().then(response => setStudents(response.data.students))
  }, [])

  return (
    <>
      <h1>Students</h1>
      {/* {students.map(student => (
        <h4>{`${student.firstName} ${student.lastName}`}</h4>
      ))} */}
    </>
  )
}

export default TeamPage
