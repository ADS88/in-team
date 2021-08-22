import { useEffect, useState } from "react"
import axios from "../../axios-config"
import Course from "../../models/course"
import CourseOverview from "./CourseOverview"
import { useHistory } from "react-router-dom"

import { Heading, Stack, Flex, useColorModeValue } from "@chakra-ui/react"
import SingleRowForm from "../ui/SingleRowForm"

const CoursesPage = () => {
  const [courses, setCourses] = useState<Course[]>([])
  const history = useHistory()

  const getAllCourses = () => {
    return axios.get<Course[]>("/course")
  }

  const addCourse = async (name: string) => {
    try {
      const response = await axios.post("course", { name })
      setCourses(prevCourses => [
        ...prevCourses,
        { name, id: response.data.id },
      ])
    } catch (error) {
      console.log(error)
    }
  }

  useEffect(() => {
    getAllCourses().then(courses => {
      setCourses(courses.data)
    })
  }, [])

  const allCourses = courses.map(({ name, id }) => (
    <div
      onClick={() => history.push(`course/${id}`)}
      style={{ cursor: "pointer" }}
      key={id}
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
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <Heading fontSize="4xl">Courses</Heading>
      <Stack spacing="8" p="4">
        {allCourses}
        <SingleRowForm addToList={addCourse} content="course" />
      </Stack>
    </Flex>
  )
}

export default CoursesPage
