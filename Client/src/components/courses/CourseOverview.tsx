import Course from "../courses/course"

interface CourseOverviewProps {
  name: string
}

const CourseOverview = ({ name }: CourseOverviewProps) => {
  return <h1>{name}</h1>
}

export default CourseOverview
