import { SimpleGrid } from "@chakra-ui/react"
import Badge from "../ui/Badge"

const Badges = () => {
  return (
    <SimpleGrid
      gridGap="8"
      p="8"
      columns={{ sm: 1, md: 3, xl: 5 }}
      justifyContent="center"
    >
      <Badge name="vibes" count={6} />
      <Badge name="effort" count={3} />
      <Badge name="allrounder" count={2} />
      <Badge name="teamwork" count={4} />
      <Badge name="improving" count={1} />
    </SimpleGrid>
  )
}

export default Badges
