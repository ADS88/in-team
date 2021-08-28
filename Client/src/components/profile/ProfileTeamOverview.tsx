import Team from "../../models/team"

import { Flex, Text, Heading } from "@chakra-ui/react"

import TeamMemberOverview from "./TeamMemberOverview"

interface ProfileTeamOverviewProps {
  team: Team
}

const ProfileTeamOverview = ({ team }: ProfileTeamOverviewProps) => {
  return (
    <>
      <Flex
        p="50"
        width="xl"
        justifyContent="space-between"
        alignItems="center"
        direction={{ sm: "column", md: "row" }}
      >
        <Heading>Team100 </Heading>
        <Heading color="pink.500">50 Points(3rd)</Heading>
      </Flex>
      <Text fontSize="2xl" color="gray.400">
        Managed state
      </Text>

      {team.members?.map(member => (
        <TeamMemberOverview student={member} />
      ))}
    </>
  )
}

export default ProfileTeamOverview
