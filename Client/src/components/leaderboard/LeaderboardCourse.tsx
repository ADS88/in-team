import { Box, Heading } from "@chakra-ui/react"
import Course from "../../models/course"
import LeaderboardTeam from "./LeaderboardTeam"

interface LeaderboardCourseProps {
  course: Course
}

const LeaderboardCourse = ({ course }: LeaderboardCourseProps) => {
  return (
    <Box alignItems="left">
      <Heading py="4">SENG302 Leaderboard</Heading>
      {course.teams
        ?.sort(team => team.points)
        .map((team, index) => (
          <LeaderboardTeam team={team} position={index + 1} />
        ))}
    </Box>
  )
}

export default LeaderboardCourse
