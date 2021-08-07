import Card from "../ui/Card"
import { useHistory } from "react-router-dom"

interface TeamOverviewProps {
  name: string
  id: number
}

const TeamOverview = ({ name, id }: TeamOverviewProps) => {
  const history = useHistory()

  return (
    <div
      onClick={() => history.push(`/team/${id}`, {})}
      style={{ cursor: "pointer" }}
    >
      <Card title={name} />
    </div>
  )
}

export default TeamOverview
