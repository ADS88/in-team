import {
  GiHeartInside,
  GiCrown,
  GiEarthAsiaOceania,
  GiAnt,
  GiTeamIdea,
} from "react-icons/gi"
import { BadgeName } from "../../models/badge-name"
import { Icon, Text, Flex, VStack } from "@chakra-ui/react"

interface BadgeProps {
  name: BadgeName
  count: number
}

const badges = {
  vibes: GiHeartInside,
  effort: GiCrown,
  allrounder: GiEarthAsiaOceania,
  teamwork: GiAnt,
  improving: GiTeamIdea,
}

const ProfileIcon = (props: BadgeProps) => {
  const badge = badges[props.name]

  const capitalize = (string: string) =>
    string.charAt(0).toUpperCase() + string.slice(1)

  return (
    <VStack>
      <Flex align="flex-end">
        <Icon as={badge} boxSize={"40"} color={"blue.500"} />
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

export default ProfileIcon
