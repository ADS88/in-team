import axios from "../../axios-config"
import { useEffect, useState } from "react"
import Student from "../../models/student"
import { Select, Button } from "@chakra-ui/react"

interface AddStudentProps {
  courseId: number
}

export default function AddStudent({ courseId }: AddStudentProps) {
  useEffect(() => {
    getAllStudents().then(response => setAllStudents(response.data))
  }, [])

  const [allStudents, setAllStudents] = useState<Student[]>([])
  const [studentIdToAdd, setStudentIdToAdd] = useState<string>()

  const getAllStudents = () => {
    return axios.get(`student/course/${courseId}`)
  }

  const addStudentToCourse = () => {
    axios.post(`team/${courseId}/addstudent/${studentIdToAdd}`)
  }

  return (
    <>
      <Select
        name="students"
        id="students"
        onChange={e => setStudentIdToAdd(e.target.value)}
      >
        {allStudents?.map(student => (
          <option key={student.id} value={student.id}>
            {student.firstName} {student.lastName}
          </option>
        ))}
      </Select>
      <Button
        onClick={addStudentToCourse}
        bg={"blue.400"}
        color={"white"}
        _hover={{
          bg: "blue.500",
        }}
      >
        Add Student to team
      </Button>
    </>
  )
}
