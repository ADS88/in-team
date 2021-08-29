import { SimpleGrid } from "@chakra-ui/react"
import ProfileBadge from "./ProfileBadge"

const Badges = () => {
  return (
    <SimpleGrid
      gridGap="8"
      p="8"
      columns={{ sm: 1, md: 3, xl: 5 }}
      justifyContent="center"
    >
      <ProfileBadge name="vibes" count={6} />
      <ProfileBadge name="effort" count={3} />
      <ProfileBadge name="allrounder" count={2} />
      <ProfileBadge name="teamwork" count={4} />
      <ProfileBadge name="improving" count={1} />
    </SimpleGrid>
  )
}

export default Badges
