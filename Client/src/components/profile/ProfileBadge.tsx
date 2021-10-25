import { BadgeName } from "../../models/badge-name"
import { Text, Flex, VStack } from "@chakra-ui/react"
import Badge from "../ui/Badge"

interface ProfileBadgeProps {
  name: BadgeName
  count: number
}

//Component for a single badge, and the number of times a user has achieved it.
const ProfileBadge = (props: ProfileBadgeProps) => {
  const capitalize = (string: string) =>
    string.charAt(0).toUpperCase() + string.slice(1)

  return (
    <VStack>
      <Flex align="flex-end">
        <Badge name={props.name} />
        <Flex
          borderRadius={"50%"}
          borderColor="blue.500"
          borderWidth="medium"
          w="40px"
          h="40px"
          align="center"
          justifyContent="center"
        >
          <Text>x{props.count}</Text>
        </Flex>
      </Flex>
      <Text fontSize="3xl">{capitalize(props.name)}</Text>
    </VStack>
  )
}

export default ProfileBadge
