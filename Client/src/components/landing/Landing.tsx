import CallToAction from "./CallToAction"
import { Flex, useColorModeValue } from "@chakra-ui/react"

const Landing = () => {
  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <CallToAction />
    </Flex>
  )
}

export default Landing
