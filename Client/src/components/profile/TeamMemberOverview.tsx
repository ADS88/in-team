import { Flex, Text } from "@chakra-ui/react"
import Student from "../../models/student"

import ProfileIcon from "../ui/ProfileIcon"

interface TeamMemberOverviewProps {
  student: Student
}

const TeamMemberOverview = ({ student }: TeamMemberOverviewProps) => {
  return (
    <Flex align="center" gridGap="4">
      <ProfileIcon iconName={student.profileIcon} isFull={false} />
      <Text fontSize="3xl">
        {student.firstName} {student.lastName}
      </Text>
    </Flex>
  )
}
export default TeamMemberOverview
