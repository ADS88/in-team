import { Select } from "@chakra-ui/select"
import { Badge as IBadge } from "../../models/badge"
import Badge from "../ui/Badge"

import { Text, Flex, Box } from "@chakra-ui/react"
import Student from "../../models/student"

interface BadgeGiftProps {
  badge: IBadge
  students: Student[]
}

const BadgeGift = (props: BadgeGiftProps) => {
  const getDescriptionFromBadge = (badge: IBadge) => {
    switch (badge.name) {
      case "vibes":
        return (
          <Text>
            Give the{" "}
            <Text as="span" color="pink.500">
              Vibes
            </Text>{" "}
            badge for a members positivity
          </Text>
        )
      case "allrounder":
        return (
          <Text>
            Give the
            <Text as="span" color="pink.500">
              {" "}
              All rounder
            </Text>{" "}
            badge for a members contribution in all areas
          </Text>
        )

      case "effort":
        return (
          <Text>
            Give the
            <Text as="span" color="pink.500">
              {" "}
              Effort{" "}
            </Text>
            badge for a members dedication
          </Text>
        )
      case "improving":
        return (
          <Text>
            Give the
            <Text as="span" color="pink.500">
              {" "}
              Improving{" "}
            </Text>
            badge to a member who's ideas improved the team
          </Text>
        )
      case "teamwork":
        return (
          <Text>
            Give the
            <Text as="span" color="pink.500">
              {" "}
              Teamwork{" "}
            </Text>
            badge to a member for their amazing teamwork
          </Text>
        )
    }
  }

  return (
    <Flex align="center" justify="space-between" gridGap="8">
      <Badge name={props.badge.name} />
      <Box flexGrow={1}>
        {getDescriptionFromBadge(props.badge)}
        <Select placeholder="Select Member">
          {props.students?.map(student => (
            <option key={student.id} value={student.id}>
              {student.firstName} {student.lastName}
            </option>
          ))}
        </Select>
      </Box>
    </Flex>
  )
}

export default BadgeGift
