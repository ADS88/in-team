import {
  Flex,
  useColorModeValue,
  Heading,
  VStack,
  SkeletonCircle,
  Text,
} from "@chakra-ui/react"
import { IconName } from "../../models/icon-name"
import axios from "../../axios-config"
import Badges from "./Badges"
import { useEffect, useState } from "react"
import Student from "../../models/student"
import UpdateProfileIcon from "./UpdateProfileIcon"

export interface ProfilePageProps {}

const ProfilePage = () => {
  const [student, setStudent] = useState<Student | null>(null)
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

        {/* <TeamMemberOverview student={student} /> */}
      </VStack>
    </Flex>
  )
}

export default ProfilePage
