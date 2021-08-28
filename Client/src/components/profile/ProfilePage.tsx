import {
  Flex,
  useColorModeValue,
  Heading,
  VStack,
  SkeletonCircle,
} from "@chakra-ui/react"
import { IconName } from "../../models/icon-name"
import axios from "../../axios-config"
import Badges from "./Badges"
import { useEffect, useState } from "react"
import Student from "../../models/student"
import UpdateProfileIcon from "./UpdateProfileIcon"
import Team from "../../models/team"
import ProfileTeamOverview from "./ProfileTeamOverview"

export interface ProfilePageProps {}

const ProfilePage = () => {
  const [student, setStudent] = useState<Student | null>(null)
  const [teams, setTeams] = useState<Team[]>([])
  const [isLoading, setIsLoading] = useState(true)

  const updateProfileIconInUI = (newIcon: IconName) => {
    setStudent(prevStudent => {
      if (prevStudent != null) {
        return { ...prevStudent, profileIcon: newIcon }
      }
      return prevStudent
    })
  }

  useEffect(() => {
    axios.get("student/current").then(response => {
      setStudent(response.data)
      setTeams(response.data.teams)
      setIsLoading(false)
    })
  }, [])

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
      p="8"
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <VStack p="8" align="center">
        <SkeletonCircle size="80" isLoaded={isLoading === false}>
          {student?.profileIcon && (
            <UpdateProfileIcon
              currentIcon={student?.profileIcon}
              updateIconInUI={updateProfileIconInUI}
            />
          )}
        </SkeletonCircle>

        <Heading>
          {student?.firstName} {student?.lastName}
        </Heading>
        <Badges />

        {teams.map(team => (
          <ProfileTeamOverview team={team} />
        ))}
      </VStack>
    </Flex>
  )
}

export default ProfilePage
