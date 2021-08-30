import axios from "../../axios-config"
import { useState } from "react"
import Student from "../../models/student"
import { Input, Button } from "@chakra-ui/react"

interface AutoCompleteProps {
  teamId: Number
  addToTeam: (student: Student) => void
}

const AutoComplete = (props: AutoCompleteProps) => {
  const [search, setSearch] = useState("")
  const [suggestions, setSuggestions] = useState<Student[]>([])
  const [display, setDisplay] = useState(false)
  const [selectedStudent, setSelectedStudent] = useState<Student | null>(null)

  const onChangeHandler = (searchString: string) => {
    setSearch(searchString)
    if (searchString.length > 0) {
      updateSuggestedUsers()
    }
  }

  const onSubmitHandler = () => {
    if (selectedStudent !== null) {
      props.addToTeam(selectedStudent)
      setSelectedStudent(null)
      setSearch("")
      setSuggestions([])
    }
  }

  const onSuggestionClickedHandler = (student: Student) => {
    setSelectedStudent(student)
    setSearch(`${student.firstName} ${student.lastName}`)
  }

  const updateSuggestedUsers = () => {
    axios
      .get(`student/course/${props.teamId}`, { params: { search } })
      .then(response => setSuggestions(response.data))
  }

  return (
    <>
      <form>
        <Input
          placeholder="type to search"
          onChange={e => onChangeHandler(e.target.value)}
          value={search}
          onClick={() => setDisplay(true)}
          onBlur={() => setTimeout(() => setDisplay(false), 200)}
        ></Input>
        {display &&
          suggestions.map(suggestion => (
            <div
              key={suggestion.id}
              style={{ cursor: "pointer" }}
              onClick={() => onSuggestionClickedHandler(suggestion)}
            >
              <span>
                {suggestion.firstName} {suggestion.lastName}
              </span>
            </div>
          ))}
        {display && suggestions.length === 0 && search.length > 1 && (
          <div>
            <span>No students found</span>
          </div>
        )}
      </form>
      <Button
        bg={"blue.400"}
        color={"white"}
        _hover={{
          bg: "blue.500",
        }}
        onClick={onSubmitHandler}
        isDisabled={selectedStudent == null}
      >
        Add Student to team
      </Button>
    </>
  )
}

export default AutoComplete
