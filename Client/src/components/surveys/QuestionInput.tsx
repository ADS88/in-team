import Question from "../../models/question"
import {
  Slider,
  SliderTrack,
  SliderFilledTrack,
  Box,
  SliderThumb,
  Text,
  Stack,
  useColorModeValue,
} from "@chakra-ui/react"
import { useState } from "react"

import { LikertRating } from "../../models/likertrating"

export interface QuestionInputProps {
  question: Question
  updateAnswer: (questionId: number, answer: LikertRating) => void
}

//Component letting the student answer a question using a sliding likert scale
const QuestionInput = ({ question, updateAnswer }: QuestionInputProps) => {
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
      <Text color={useColorModeValue("gray.600", "gray.300")}>
        {description}
      </Text>
      <Slider
        defaultValue={3}
        min={1}
        max={5}
        step={1}
        onChange={(value: LikertRating) => {
          setDescription(states[value])
          updateAnswer(question.id, value)
        }}
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
