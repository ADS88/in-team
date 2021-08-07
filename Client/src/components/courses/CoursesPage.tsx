import { useEffect, useState } from "react"
import axios from "../../axios-config"
import Course from "./course"
import CourseOverview from "./CourseOverview"
import { useHistory } from "react-router-dom"

import { Text, Stack, Flex } from "@chakra-ui/react"
import AddCourse from "./AddCourse"

const CoursesPage = () => {
  const [courses, setCourses] = useState<Course[]>([])
  const history = useHistory()

  const getAllCourses = () => {
    return axios.get<Course[]>("/course")
  }

  const addCourse = (course: Course) => {
    setCourses(prevCourses => [...prevCourses, course])
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
    <Flex
      minH={"90vh"}
      align={"center"}
      justify={"center"}
      direction={"column"}
    >
      <Text fontSize="6xl">Courses</Text>
      <Stack spacing="8" p="4">
        {allCourses}
        <AddCourse addCourseToList={addCourse} />
      </Stack>
    </Flex>
  )
}

export default CoursesPage
