import {
  Modal,
  ModalContent,
  ModalBody,
  ModalOverlay,
  ModalHeader,
  ModalCloseButton,
  SimpleGrid,
  useDisclosure,
} from "@chakra-ui/react"
import { IconName } from "../../models/icon-name"
import ProfileIcon from "../ui/ProfileIcon"
import { allNames } from "../../models/icon-name"
import axios from "../../axios-config"

interface UpdateProfileIconProps {
  currentIcon: IconName
  updateIconInUI: (newIcon: IconName) => void
}

const UpdateProfileIcon = (props: UpdateProfileIconProps) => {
  const { isOpen, onOpen, onClose } = useDisclosure()

  const updateProfileIcon = (newIcon: IconName) => {
    axios
      .patch("student/current/profile-icon", { profileIcon: newIcon })
      .then(() => {
        props.updateIconInUI(newIcon)
        onClose()
      })
  }

  return (
    <>
      <div onClick={onOpen}>
        <ProfileIcon iconName={props.currentIcon} isFull={true} />
      </div>
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
    </>
  )
}

export default UpdateProfileIcon
