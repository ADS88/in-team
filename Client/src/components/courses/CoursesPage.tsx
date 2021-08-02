import { useEffect, useState } from "react"
import axios from "../../axios-config"
import Course from "./course"
import CourseOverview from "./CourseOverview"
import { useHistory } from "react-router-dom"

import { Text, Stack } from "@chakra-ui/react"

const CoursesPage = () => {
  const [courses, setCourses] = useState<Course[]>([])
  const history = useHistory()

  const getAllCourses = () => {
    return axios.get<Course[]>("/course")
  }

  useEffect(() => {
    getAllCourses().then(courses => {
      setCourses(courses.data)
      console.log(courses)
    })
  }, [])

  const allCourses = courses.map(({ name, id }) => (
    <div
      onClick={() => history.push(`course/${id}`)}
      style={{ cursor: "pointer" }}
    >
      <CourseOverview name={name} key={id} />
    </div>
  ))

  return (
    <>
      <Text fontSize="6xl">All Courses</Text>
      <Stack spacing="8" p="4">
        {allCourses}
      </Stack>
    </>
  )
}

export default CoursesPage
