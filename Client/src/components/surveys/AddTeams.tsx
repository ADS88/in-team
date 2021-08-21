import { useEffect, useState } from "react"
import Team from "../../models/team"
import Select from "react-select"
import axios from "../../axios-config"

import { FormLabel, FormControl } from "@chakra-ui/react"

interface AddTeamsProps {
  updateTeams: (newTeamIds: number[]) => void
}

const AddTeams = (props: AddTeamsProps) => {
  const [allTeams, setAllTeams] = useState<Team[]>([])

  useEffect(() => {
    const getTeams = () => {
      return axios.get("team")
    }

    getTeams().then(response => setAllTeams(response.data))
  }, [])

  const options = allTeams.map(team => {
    return { value: team.id, label: team.name }
  })

  return (
    <FormControl>
      <FormLabel>Select Recipients</FormLabel>
      <Select
        isMulti
        options={options}
        style={{ width: 500 }}
        onChange={selection => props.updateTeams(selection.map(s => s.value))}
      />
    </FormControl>
  )
}

export default AddTeams
