import Card from "../ui/Card"

interface CourseOverviewProps {
  name: string
}

const CourseOverview = ({ name }: CourseOverviewProps) => {
  return <Card title={name} />
}

export default CourseOverview
