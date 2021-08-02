import Course from "../courses/course"
import { useEffect } from "react"
import axios from "../../axios-config"

interface CourseDetailProps {
  id: string
}

const CourseDetailPage = ({ id }: CourseDetailProps) => {
  const getCourse = () => {
    return axios.get(`course/${id}`)
  }

  useEffect(() => {
    getCourse().then(x => console.log(x))
  }, [])

  return <h1>Looking at course {id}!</h1>
}

export default CourseDetailPage
