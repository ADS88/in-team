import ReactDatePicker, { ReactDatePickerProps } from "react-datepicker"
import { useColorMode } from "@chakra-ui/react"

import "react-datepicker/dist/react-datepicker.css"
import "../../stylesheets/date-picker.css"
import { ModifiersArray } from "typescript"

//Reusable date picker component.
function DatePicker(props: ReactDatePickerProps<ModifiersArray>) {
  const { isClearable = false, showPopperArrow = false, ...rest } = props
  const isLight = useColorMode().colorMode === "light"

  return (
    <div className={isLight ? "light-theme" : "dark-theme"}>
      <ReactDatePicker
        isClearable={isClearable}
        showPopperArrow={showPopperArrow}
        className="react-datapicker__input-text"
        {...rest}
      />
    </div>
  )
}

export default DatePicker
