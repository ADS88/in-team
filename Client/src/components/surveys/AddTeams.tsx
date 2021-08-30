import { useEffect, useState } from "react"
import Team from "../../models/team"
import Select from "../ui/Select"
import axios from "../../axios-config"
import { OptionsType } from "react-select"

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

  const onSelectionChangeHandler = (
    selection: OptionsType<{
      value: number
      label: string
    }>
  ) => {
    props.updateTeams(selection.map(s => s.value))
  }

  return (
    <FormControl>
      <FormLabel>Select Recipients</FormLabel>
      <Select options={options} onChange={onSelectionChangeHandler} />
    </FormControl>
  )
}

export default AddTeams
