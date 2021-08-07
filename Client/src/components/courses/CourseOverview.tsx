import Card from "../ui/Card"
import { Text } from "@chakra-ui/react"

interface CourseOverviewProps {
  name: string
}

const CourseOverview = ({ name }: CourseOverviewProps) => {
  return <Card title={name} />
}

export default CourseOverview
