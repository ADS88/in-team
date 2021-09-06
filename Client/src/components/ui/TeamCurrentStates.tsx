import { Flex, Text } from "@chakra-ui/react"
import { useEffect, useState } from "react"
import axios from "../../axios-config"
import AchievedState from "../../models/achievedstate"

interface TeamsCurrentStatesProps {
  teamId: string
}

const TeamsCurrentStates = ({ teamId }: TeamsCurrentStatesProps) => {
  useEffect(() => {
    axios
      .get(`team/${teamId}/currentstates`)
      .then(response => setAchievedStates(response.data.achievedStates))
  }, [teamId])

  const [achievedStates, setAchievedStates] = useState<AchievedState[]>([])

  return (
    <>
      {achievedStates.map(achievedState => (
        <Flex justifyContent="left" direction={"row"} gridGap="4">
          <Text fontSize="xl">{achievedState.alphaName}: </Text>
          <Text fontSize="xl" color="pink.500">
            {achievedState.stateName}
          </Text>
        </Flex>
      ))}

      {achievedStates.length === 0 && (
        <Text fontSize="2xl" color="gray.400">
          No states achieved{" "}
        </Text>
      )}
    </>
  )
}

export default TeamsCurrentStates
