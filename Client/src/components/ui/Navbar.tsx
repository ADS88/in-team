import {
  Box,
  Flex,
  Text,
  IconButton,
  Button,
  Stack,
  Collapse,
  Icon,
  Link,
  useColorModeValue,
  useBreakpointValue,
  useDisclosure,
  useColorMode,
} from "@chakra-ui/react"
import {
  HamburgerIcon,
  CloseIcon,
  ChevronDownIcon,
  SunIcon,
  MoonIcon,
} from "@chakra-ui/icons"

import { Link as RouterLink } from "react-router-dom"

import { useHistory } from "react-router-dom"
import { AuthContext } from "../../store/auth-context"
import { useContext } from "react"

export default function WithSubnavigation() {
  const { colorMode, toggleColorMode } = useColorMode()
  const { isOpen, onToggle } = useDisclosure()
  const history = useHistory()
  const authContext = useContext(AuthContext)
  let isLoggedIn = authContext.isLoggedIn

  const loginButton = (
    <Button
      as={RouterLink}
      fontSize={"sm"}
      fontWeight={400}
      variant={"link"}
      to={"login"}
    >
      Sign In
    </Button>
  )

  const toggeColorModeButton = (
    <IconButton
      aria-label="Color mode toggle button"
      icon={colorMode === "light" ? <SunIcon /> : <MoonIcon />}
      isRound={true}
      size="sm"
      alignSelf="flex-end"
      onClick={toggleColorMode}
    />
  )

  const signUpButton = (
    <Button
      display={{ base: "none", md: "inline-flex" }}
      to={"register"}
      as={RouterLink}
      fontSize={"sm"}
      fontWeight={600}
      color={"white"}
      bg={"pink.400"}
      href={"register"}
      _hover={{
        bg: "pink.300",
      }}
    >
      Sign Up
    </Button>
  )

  const logoutButton = (
    <Button
      as={RouterLink}
      to="/login"
      fontSize={"sm"}
      fontWeight={400}
      variant={"link"}
      onClick={() => {
        authContext.logout()
      }}
    >
      Sign out
    </Button>
  )

  const coursesButton = (
    <Button
      display={{ base: "none", md: "inline-flex" }}
      as={RouterLink}
      fontSize={"sm"}
      fontWeight={400}
      variant={"link"}
      to={"/courses"}
    >
      Courses
    </Button>
  )

  const alphasButton = (
    <Button
      display={{ base: "none", md: "inline-flex" }}
      as={RouterLink}
      fontSize={"sm"}
      fontWeight={400}
      variant={"link"}
      to={"/alphas"}
    >
      Alphas
    </Button>
  )

  const surveysButton = (
    <Button
      display={{ base: "none", md: "inline-flex" }}
      as={RouterLink}
      fontSize={"sm"}
      fontWeight={400}
      variant={"link"}
      to={"/surveys"}
    >
      Surveys
    </Button>
  )

  return (
    <Box>
      <Flex
        bg={useColorModeValue("white", "gray.800")}
        color={useColorModeValue("gray.600", "white")}
        minH={"60px"}
        py={{ base: 2 }}
        px={{ base: 4 }}
        borderBottom={1}
        borderStyle={"solid"}
        borderColor={useColorModeValue("gray.200", "gray.900")}
        align={"center"}
      >
        <Flex
          flex={{ base: 1, md: "auto" }}
          ml={{ base: -2 }}
          display={{ base: "flex", md: "none" }}
        >
          <IconButton
            onClick={onToggle}
            icon={
              isOpen ? <CloseIcon w={3} h={3} /> : <HamburgerIcon w={5} h={5} />
            }
            variant={"ghost"}
            aria-label={"Toggle Navigation"}
          />
        </Flex>
        <Flex flex={{ base: 1 }} justify={{ base: "center", md: "start" }}>
          <Flex align="center" gridGap="2">
            <Text
              onClick={() => history.push("/")}
              _hover={{
                cursor: "pointer",
              }}
              textAlign={useBreakpointValue({ base: "center", md: "left" })}
              fontFamily={"heading"}
              color={useColorModeValue("gray.800", "white")}
            >
              <b>InTeam</b>
            </Text>
            {toggeColorModeButton}
          </Flex>
        </Flex>

        <Stack
          flex={{ base: 1, md: 0 }}
          justify={"flex-end"}
          direction={"row"}
          spacing={6}
        >
          {isLoggedIn ? (
            <>
              {surveysButton}
              {alphasButton}
              {coursesButton}
              {logoutButton}
            </>
          ) : (
            <>
              {loginButton}
              {signUpButton}
            </>
          )}
        </Stack>
      </Flex>

      <Collapse in={isOpen} animateOpacity>
        <MobileNav />
      </Collapse>
    </Box>
  )
}

const MobileNav = () => {
  const authContext = useContext(AuthContext)
  const NAV_ITEMS: Array<NavItem> = []
  if (authContext.isLoggedIn) {
    NAV_ITEMS.push({
      label: "Courses",
      href: "courses",
    })
    NAV_ITEMS.push({
      label: "Alphas",
      href: "alphas",
    })
    NAV_ITEMS.push({
      label: "Surveys",
      href: "surveys",
    })
  } else {
    NAV_ITEMS.push({
      label: "Register",
      href: "register",
    })
  }

  return (
    <Stack
      bg={useColorModeValue("white", "gray.800")}
      p={4}
      display={{ md: "none" }}
    >
      {NAV_ITEMS.map(navItem => (
        <MobileNavItem key={navItem.label} {...navItem} />
      ))}
    </Stack>
  )
}

const MobileNavItem = ({ label, children, href }: NavItem) => {
  const { isOpen, onToggle } = useDisclosure()

  return (
    <Stack spacing={4} onClick={children && onToggle}>
      <Flex
        py={2}
        as={Link}
        href={href ?? "#"}
        justify={"space-between"}
        align={"center"}
        _hover={{
          textDecoration: "none",
        }}
      >
        <Text
          fontWeight={600}
          color={useColorModeValue("gray.600", "gray.200")}
        >
          {label}
        </Text>
        {children && (
          <Icon
            as={ChevronDownIcon}
            transition={"all .25s ease-in-out"}
            transform={isOpen ? "rotate(180deg)" : ""}
            w={6}
            h={6}
          />
        )}
      </Flex>

      <Collapse in={isOpen} animateOpacity style={{ marginTop: "0!important" }}>
        <Stack
          mt={2}
          pl={4}
          borderLeft={1}
          borderStyle={"solid"}
          borderColor={useColorModeValue("gray.200", "gray.700")}
          align={"start"}
        >
          {children &&
            children.map(child => (
              <Link key={child.label} py={2} href={child.href}>
                {child.label}
              </Link>
            ))}
        </Stack>
      </Collapse>
    </Stack>
  )
}

interface NavItem {
  label: string
  subLabel?: string
  children?: Array<NavItem>
  href?: string
}
