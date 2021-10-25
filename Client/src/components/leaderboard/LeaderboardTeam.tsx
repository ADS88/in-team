import { SimpleGrid, Text } from "@chakra-ui/react"
import Team from "../../models/team"

interface LeaderboardTeamProps {
  team: Team
  position: number
}

//Shows a single teams position within the leaderboard
const LeaderboardTeam = ({ team, position }: LeaderboardTeamProps) => {
  return (
    <SimpleGrid columns={2} gridGap="16" py="4">
      <Text fontSize="4xl">
        {position}: {team.name}
      </Text>
      <Text fontSize="4xl" color="pink.500">
        {team.points} Points
      </Text>
    </SimpleGrid>
  )
}

export default LeaderboardTeam
