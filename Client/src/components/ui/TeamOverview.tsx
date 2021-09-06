import Team from "../../models/team"
import { Flex, Heading } from "@chakra-ui/react"
import TeamMemberOverview from "../profile/TeamMemberOverview"
import TeamsCurrentStates from "./TeamCurrentStates"

interface TeamOverviewProps {
  team: Team
}

const TeamOverview = ({ team }: TeamOverviewProps) => {
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

      <TeamsCurrentStates teamId={team.id.toString()} />

      {team.members?.map(member => (
        <TeamMemberOverview student={member} />
      ))}
    </>
  )
}

export default TeamOverview
