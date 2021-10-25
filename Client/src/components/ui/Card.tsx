import { useColorModeValue, Box, Text } from "@chakra-ui/react"

interface CardProps {
  title: string
}

//Reusable card component, provides a box shadow and padding.
const Card = ({ title }: CardProps) => {
  return (
    <Box
      minW={"400px"}
      w={"full"}
      bg={useColorModeValue("white", "gray.900")}
      boxShadow={"md"}
      rounded={"lg"}
      p={6}
      textAlign={"center"}
    >
      <Text fontSize="xl">{title}</Text>
    </Box>
  )
}

export default Card
