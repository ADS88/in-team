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
import { useContext, useEffect, useState } from "react"
import Student from "../../models/student"
import Team from "../../models/team"
import ProfileTeamOverview from "./ProfileTeamOverview"
import { useParams } from "react-router"
import { AuthContext } from "../../store/auth-context"
import ProfileIconDisplay from "./ProfileIconDisplay"
import { Badge } from "../../models/badge"

export interface ProfilePageProps {}

const ProfilePage = () => {
  const authContext = useContext(AuthContext)

  let { id } = useParams<{ id: string }>()
  if (id == null && authContext.userId != null) {
    id = authContext.userId
  }
  const viewingOwnProfile = id === authContext.userId

  const [student, setStudent] = useState<Student | null>(null)
  const [badges, setBadges] = useState<Badge[]>([])
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
    axios.get(`student/${id}`).then(response => {
      setStudent(response.data)
      setTeams(response.data.teams)
      setIsLoading(false)
    })
    axios.get(`student/${id}/badges`).then(response => setBadges(response.data))
  }, [id])

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
            <ProfileIconDisplay
              currentIcon={student?.profileIcon}
              updateIconInUI={updateProfileIconInUI}
              profileId={id}
              isViewingOwnProfile={viewingOwnProfile}
            />
          )}
        </SkeletonCircle>

        <Heading>
          {student?.firstName} {student?.lastName}
        </Heading>
        <Badges badges={badges} />

        {teams.map(team => (
          <ProfileTeamOverview team={team} />
        ))}
      </VStack>
    </Flex>
  )
}

export default ProfilePage
