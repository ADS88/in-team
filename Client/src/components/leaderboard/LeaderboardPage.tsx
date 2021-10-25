import axios from "../../axios-config"
import { Stack, useColorModeValue } from "@chakra-ui/react"
import LeaderboardCourse from "./LeaderboardCourse"
import { useEffect, useState } from "react"
import Course from "../../models/course"

//A page that shows the leaderboard for all courses
const LeaderboardPage = () => {
  const [courses, setCourses] = useState<Course[]>([])

  useEffect(() => {
    axios.get("course").then(response => setCourses(response.data))
  }, [])

  return (
    <Stack
      bg={useColorModeValue("gray.50", "gray.800")}
      minH={"90vh"}
      w={"100vw"}
      spacing={8}
      mx={"auto"}
      py={12}
      px={6}
      align="center"
    >
      {courses.map(course => (
        <LeaderboardCourse course={course} key={course.id} />
      ))}
    </Stack>
  )
}

export default LeaderboardPage
