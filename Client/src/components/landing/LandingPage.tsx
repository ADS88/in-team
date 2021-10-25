import CallToAction from "./CallToAction"
import { Flex, useColorModeValue } from "@chakra-ui/react"

//Container for the main page the user sees upon first visiting the website
function LandingPage() {
  return (
    <Flex
      minH={"95vh"}
      align={"center"}
      justify={"center"}
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <CallToAction />
    </Flex>
  )
}

export default LandingPage
