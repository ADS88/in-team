import { IconName } from "../../models/icon-name"
import {
  GiAnglerFish,
  GiHamburger,
  GiLabradorHead,
  GiLion,
  GiOctopus,
  GiSamuraiHelmet,
  GiSunbeams,
  GiWalrusHead,
  GiMuscleUp,
  GiSushis,
  GiKiwiBird,
  GiMantaRay,
  GiEgyptianBird,
  GiGiantSquid,
  GiInspiration,
  GiNinjaVelociraptor,
  GiPodiumWinner,
  GiBackstab,
  GiBuffaloHead,
  GiSnowman,
  GiSpectre,
  GiSpaceSuit,
  GiSpiderAlt,
  GiTotemHead,
} from "react-icons/gi"
import { Icon } from "@chakra-ui/react"

interface ProfileIconProps {
  iconName: IconName
}

const ProfileIcon = (props: ProfileIconProps) => {
  switch (props.iconName) {
    case "angler":
      return <Icon as={GiAnglerFish} boxSize="xs" />
    case "hamburger":
      return <Icon as={GiHamburger} boxSize="xs" />
    case "dog":
      return <Icon as={GiLabradorHead} boxSize="xs" />
    case "lion":
      return <Icon as={GiLion} boxSize="xs" />
    case "octopus":
      return <Icon as={GiOctopus} boxSize="xs" />
    case "samurai":
      return <Icon as={GiSamuraiHelmet} boxSize="xs" />
    case "sunbeams":
      return <Icon as={GiSunbeams} boxSize="xs" />
    case "walrus":
      return <Icon as={GiWalrusHead} boxSize="xs" />
    case "muscles":
      return <Icon as={GiMuscleUp} boxSize="xs" />
    case "sushi":
      return <Icon as={GiSushis} boxSize="xs" />
    case "kiwi":
      return <Icon as={GiKiwiBird} boxSize="xs" />
    case "mantaray":
      return <Icon as={GiMantaRay} boxSize="xs" />
    case "bird":
      return <Icon as={GiEgyptianBird} boxSize="xs" />
    case "squid":
      return <Icon as={GiGiantSquid} boxSize="xs" />
    case "inspiration":
      return <Icon as={GiInspiration} boxSize="xs" />
    case "dinosaur":
      return <Icon as={GiNinjaVelociraptor} boxSize="xs" />
    case "winner":
      return <Icon as={GiPodiumWinner} boxSize="xs" />
    case "backstab":
      return <Icon as={GiBackstab} boxSize="xs" />
    case "buffalo":
      return <Icon as={GiBuffaloHead} boxSize="xs" />
    case "snowman":
      return <Icon as={GiSnowman} boxSize="xs" />
    case "spectre":
      return <Icon as={GiSpectre} boxSize="xs" />
    case "spider":
      return <Icon as={GiSpiderAlt} boxSize="xs" />
    case "spacesuit":
      return <Icon as={GiSpaceSuit} boxSize="xs" />
    case "totem":
      return <Icon as={GiTotemHead} boxSize="xs" />
    default:
      return <Icon as={GiAnglerFish} boxSize="xs" />
  }
}

export default ProfileIcon
