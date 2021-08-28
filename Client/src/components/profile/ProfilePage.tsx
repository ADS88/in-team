import { Flex, useColorModeValue, Heading, Text } from "@chakra-ui/react"
import ProfileIcon from "../ui/ProfileIcon"

export interface ProfilePageProps {}

const ProfilePage = () => {
  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
      p="8"
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <ProfileIcon iconName="angler" />
      <Heading>Andrew Sturman</Heading>
      <Text textStyle="xl">Badges</Text>
    </Flex>
  )
}

export default ProfilePage
