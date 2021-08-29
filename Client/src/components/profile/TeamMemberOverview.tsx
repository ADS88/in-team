import { Flex, Text } from "@chakra-ui/react"
import { useHistory } from "react-router"
import Student from "../../models/student"

import ProfileIcon from "../ui/ProfileIcon"

interface TeamMemberOverviewProps {
  student: Student
}

const TeamMemberOverview = ({ student }: TeamMemberOverviewProps) => {
  const history = useHistory()

  const goToProfile = () => history.push(`/profile/${student.id}`)

  return (
    <div onClick={goToProfile} style={{ cursor: "pointer" }}>
      <Flex align="center" gridGap="4" p="4">
        <ProfileIcon iconName={student.profileIcon} isFull={false} />
        <Text fontSize="3xl">
          {student.firstName} {student.lastName}
        </Text>
      </Flex>
    </div>
  )
}
export default TeamMemberOverview
