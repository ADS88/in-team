import {
  Box,
  Center,
  Heading,
  Text,
  Stack,
  useColorModeValue,
} from "@chakra-ui/react"
import Survey from "../../models/survey"

interface SurveyOverviewProps {
  survey: Survey
}

export default function SurveyOverview({ survey }: SurveyOverviewProps) {
  return (
    <Box
      w={"lg"}
      bg={useColorModeValue("white", "gray.900")}
      boxShadow={"2xl"}
      rounded={"md"}
      p={6}
      overflow={"hidden"}
    >
      <Stack>
        <Heading
          color={useColorModeValue("gray.700", "white")}
          fontSize={"2xl"}
          fontFamily={"body"}
        >
          {survey.name}
        </Heading>
        <Stack direction="row">
          <Text
            color={"pink.300"}
            textTransform={"uppercase"}
            fontWeight={800}
            fontSize={"sm"}
            letterSpacing={1.1}
          >
            Team:
          </Text>
          <Text fontSize="sm" color={"gray.500"}>
            Managed, Adjourning
          </Text>
        </Stack>
        <Stack direction="row">
          <Text
            color={"blue.300"}
            textTransform={"uppercase"}
            fontWeight={800}
            fontSize={"sm"}
            letterSpacing={1.1}
          >
            Team:
          </Text>
          <Text fontSize="sm" color={"gray.500"}>
            Team100, Team200, Team300
          </Text>
        </Stack>
      </Stack>
      <Stack
        mt={6}
        direction={"row"}
        spacing={4}
        align={"center"}
        justifyContent="space-between"
      >
        <Stack direction={"column"} spacing={0} fontSize={"sm"}>
          <Text fontWeight={600}>Opened</Text>
          <Text color={"gray.500"}>{survey.openingDate.toDateString()}</Text>
        </Stack>
        <Stack direction={"column"} spacing={0} fontSize={"sm"}>
          <Text fontWeight={600}>Closing</Text>
          <Text color={"gray.500"}>{survey.closingDate.toDateString()}</Text>
        </Stack>
      </Stack>
    </Box>
  )
}
