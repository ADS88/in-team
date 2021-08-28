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
  GiMonkey,
  GiLuckyFisherman,
  GiRobotAntennas,
  GiPumpkinLantern,
} from "react-icons/gi"
import { Icon } from "@chakra-ui/react"

interface ProfileIconProps {
  iconName: IconName
  isFull: boolean
}

const icons = {
  angler: GiAnglerFish,
  hamburger: GiHamburger,
  dog: GiLabradorHead,
  lion: GiLion,
  octopus: GiOctopus,
  samurai: GiSamuraiHelmet,
  sunbeams: GiSunbeams,
  walrus: GiWalrusHead,
  muscles: GiMuscleUp,
  sushi: GiSushis,
  kiwi: GiKiwiBird,
  mantaray: GiMantaRay,
  bird: GiEgyptianBird,
  squid: GiGiantSquid,
  inspiration: GiInspiration,
  dinosaur: GiNinjaVelociraptor,
  winner: GiPodiumWinner,
  backstab: GiBackstab,
  buffalo: GiBuffaloHead,
  snowman: GiSnowman,
  spectre: GiSpectre,
  spider: GiSpiderAlt,
  spacesuit: GiSpaceSuit,
  totem: GiTotemHead,
  monkey: GiMonkey,
  fisherman: GiLuckyFisherman,
  pumpkin: GiPumpkinLantern,
  robot: GiRobotAntennas,
}

const ProfileIcon = (props: ProfileIconProps) => {
  const icon = icons[props.iconName]

  return <Icon as={icon} boxSize={props.isFull ? "80" : "20"} />
}

export default ProfileIcon
