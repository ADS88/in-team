import Team from "../courses/team"
import { useEffect, useState } from "react"
import axios from "../../axios-config"
import { RouteComponentProps, useHistory } from "react-router"
import { Button } from "@chakra-ui/react"

const CourseDetailPage: React.FunctionComponent<RouteComponentProps<any>> =
  props => {
    const [teams, setTeams] = useState<Team[]>([])
    const history = useHistory()

    const id = props.match.params.id
    const getCourse = () => {
      return axios.get(`course/${id}`)
    }

    useEffect(() => {
      getCourse().then(response => setTeams(response.data.teams))
    }, [teams])

    const addTeam = () => {
      // const team: Team =
      axios.post("team", { name: "cool name", courseId: id })
      // .then(() => setTeams(prevTeams => [...prevTeams, team]))
    }

    return (
      <>
        <h1>Looking at course {id}!</h1>
        <Button onClick={addTeam}>Add team</Button>
        <h2>Teams</h2>
        {teams.map(team => (
          <div
            onClick={() => history.push(`/team/${team.id}`, {})}
            style={{ cursor: "pointer" }}
          >
            <h4>{team.name}</h4>
          </div>
        ))}
      </>
    )
  }

export default CourseDetailPage
