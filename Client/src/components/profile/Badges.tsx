import { SimpleGrid } from "@chakra-ui/react"
import { Badge } from "../../models/badge"
import ProfileBadge from "./ProfileBadge"

interface BadgesProps {
  badges: Badge[]
}

//Shows a users badges, and how many of each badge the user has received.
const Badges = (props: BadgesProps) => {
  return (
    <SimpleGrid
      gridGap="8"
      p="8"
      columns={{ sm: 1, md: 3, xl: 5 }}
      justifyContent="center"
    >
      {props.badges.map(badge => (
        <ProfileBadge
          name={badge.name}
          count={badge.count ?? 0}
          key={badge.id}
        />
      ))}
    </SimpleGrid>
  )
}

export default Badges
