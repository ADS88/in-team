import Team from "../../models/team"
import { Flex, Text, Heading } from "@chakra-ui/react"
import TeamMemberOverview from "../profile/TeamMemberOverview"
import { useEffect, useState } from "react"
import axios from "../../axios-config"
import AchievedState from "../../models/achievedstate"

interface TeamOverviewProps {
  team: Team
}

const TeamOverview = ({ team }: TeamOverviewProps) => {
  useEffect(() => {
    axios
      .get(`team/${team.id}/currentstates`)
      .then(response => setAchievedStates(response.data.achievedStates))
  }, [team.id])

  const [achievedStates, setAchievedStates] = useState<AchievedState[]>([])

  return (
    <>
      <Flex
        p="50"
        width="xl"
        justifyContent="space-between"
        alignItems="center"
        direction={{ sm: "column", md: "row" }}
      >
        <Heading>{team.name}</Heading>
        <Heading color="pink.500">{team.points} Points</Heading>
      </Flex>

      {achievedStates.map(achievedState => (
        <Flex justifyContent="center" direction={"row"} gridGap="4">
          <Text fontSize="xl">{achievedState.alphaName}: </Text>
          <Text fontSize="xl" color="pink.500">
            {achievedState.stateName}
          </Text>
        </Flex>
      ))}

      {achievedStates.length === 0 && (
        <Text fontSize="2xl" color="gray.400">
          No states achieved{" "}
        </Text>
      )}

      {team.members?.map(member => (
        <TeamMemberOverview student={member} />
      ))}
    </>
  )
}

export default TeamOverview