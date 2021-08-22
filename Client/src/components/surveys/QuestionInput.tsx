import Question from "../../models/question"
import {
  Slider,
  SliderTrack,
  SliderFilledTrack,
  Box,
  SliderThumb,
  Text,
  Stack,
} from "@chakra-ui/react"
import { useState } from "react"

export interface QuestionInputProps {
  question: Question
}

type LikertAnswer = 1 | 2 | 3 | 4 | 5

const QuestionInput = ({ question }: QuestionInputProps) => {
  const states = {
    1: "Strongly disagree",
    2: "Disagree",
    3: "Neither agree or disagree",
    4: "Agree",
    5: "Strongly agree",
  }

  const [description, setDescription] = useState(states["3"])

  return (
    <Stack align="center">
      <Text fontSize="xl">{question.content}</Text>
      <Text color={"gray.600"}>{description}</Text>
      <Slider
        defaultValue={3}
        min={1}
        max={5}
        step={1}
        onChange={(value: LikertAnswer) => setDescription(states[value])}
      >
        <SliderTrack bg="pink.100">
          <Box position="relative" right={10} />
          <SliderFilledTrack bg="pink.300" />
        </SliderTrack>
        <SliderThumb boxSize={6} />
      </Slider>
    </Stack>
  )
}

export default QuestionInput
