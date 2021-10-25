import { Box, Heading, Text, Stack, useColorModeValue } from "@chakra-ui/react"
import Survey from "../../models/survey"

interface SurveyOverviewProps {
  survey: Survey
}

//Component that shows a basic overview of a survey including it's opening/closing dates, name, and assigned teams.
export default function SurveyOverview({ survey }: SurveyOverviewProps) {
  const teamNames = survey.teams?.map(team => team.name).join(", ")

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
        <Text
          color={"blue.300"}
          textTransform={"uppercase"}
          fontWeight={800}
          fontSize={"sm"}
          letterSpacing={1.1}
        >
          {teamNames}
        </Text>
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
