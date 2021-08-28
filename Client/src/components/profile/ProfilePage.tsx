import {
  Flex,
  useColorModeValue,
  Heading,
  Text,
  useDisclosure,
  Modal,
  ModalContent,
  ModalBody,
  ModalOverlay,
  ModalHeader,
  ModalCloseButton,
  SimpleGrid,
  SkeletonCircle,
  VStack,
  Box,
} from "@chakra-ui/react"
import { IconName } from "../../models/icon-name"
import ProfileIcon from "../ui/ProfileIcon"
import { allNames } from "../../models/icon-name"
import axios from "../../axios-config"

import { useEffect, useState } from "react"
import Badge from "../ui/Badge"

export interface ProfilePageProps {}

const ProfilePage = () => {
  const { isOpen, onOpen, onClose } = useDisclosure()
  const [icon, setIcon] = useState<IconName>("angler")
  const [isLoading, setIsLoading] = useState(true)

  const updateProfileIcon = (newIcon: IconName) => {
    axios
      .patch("student/current/profile-icon", { profileIcon: newIcon })
      .then(() => {
        setIcon(newIcon)
        onClose()
      })
  }

  useEffect(() => {
    axios.get("student/current").then(response => {
      setIcon(response.data.profileIcon)
      setIsLoading(false)
    })
  }, [])

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
      p="8"
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <VStack p="8">
        <SkeletonCircle size="80" isLoaded={isLoading === false}>
          <div onClick={onOpen}>
            <ProfileIcon iconName={icon} isFull={true} />
          </div>
        </SkeletonCircle>
        <Modal
          size="2xl"
          blockScrollOnMount={false}
          isOpen={isOpen}
          onClose={onClose}
        >
          <ModalOverlay />
          <ModalContent>
            <ModalHeader>Profile Icon</ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              <SimpleGrid columns={4} gap="8">
                {allNames.map(name => (
                  <div onClick={() => updateProfileIcon(name)}>
                    <ProfileIcon iconName={name} isFull={false} key={name} />
                  </div>
                ))}
              </SimpleGrid>
            </ModalBody>
          </ModalContent>
        </Modal>
        <Heading>Andrew Sturman</Heading>

        {/* <Text fontSize="3xl" color="blue.500">
        Badges
      </Text> */}
        <SimpleGrid
          gridGap="8"
          p="8"
          columns={{ sm: 1, md: 3, xl: 5 }}
          justifyContent="center"
        >
          <Badge name="vibes" count={6} />
          <Badge name="effort" count={3} />
          <Badge name="allrounder" count={2} />
          <Badge name="teamwork" count={4} />
          <Badge name="improving" count={1} />
        </SimpleGrid>
      </VStack>
    </Flex>
  )
}

export default ProfilePage
