import {
  GiHeartInside,
  GiCrown,
  GiEarthAsiaOceania,
  GiAnt,
  GiTeamIdea,
} from "react-icons/gi"
import { BadgeName } from "../../models/badge-name"
import { Icon } from "@chakra-ui/react"

interface BadgeProps {
  name: BadgeName
}

const badges = {
  vibes: GiHeartInside,
  effort: GiCrown,
  allrounder: GiEarthAsiaOceania,
  teamwork: GiAnt,
  improving: GiTeamIdea,
}

//Reusable bade component, showing a singular badge.
const Badge = (props: BadgeProps) => {
  const badge = badges[props.name]

  return <Icon as={badge} boxSize={"40"} color={"blue.500"} />
}

export default Badge
