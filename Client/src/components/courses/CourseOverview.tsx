import Card from "../ui/Card"

interface CourseOverviewProps {
  name: string
}

//Component to show a basic overview of a course
const CourseOverview = ({ name }: CourseOverviewProps) => {
  return <Card title={name} />
}

export default CourseOverview
