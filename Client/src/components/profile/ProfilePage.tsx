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
} from "@chakra-ui/react"
import { IconName } from "../../models/icon-name"
import ProfileIcon from "../ui/ProfileIcon"
import { allNames } from "../../models/icon-name"

import { useState } from "react"

export interface ProfilePageProps {}

const ProfilePage = () => {
  const { isOpen, onOpen, onClose } = useDisclosure()
  const [icon, setIcon] = useState<IconName>("angler")

  const updateProfileIcon = (newIcon: IconName) => {
    setIcon(newIcon)
    onClose()
  }

  return (
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
      p="8"
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <div onClick={onOpen}>
        <ProfileIcon iconName={icon} isFull={true} />
      </div>
      <Heading>Andrew Sturman</Heading>
      <Text fontSize="3xl" color="blue.500">
        Badges
      </Text>

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
    </Flex>
  )
}

export default ProfilePage
