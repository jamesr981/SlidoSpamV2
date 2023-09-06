import React, {useState} from "react";

interface SlidoSpammerControlProps {
    inputType: React.HTMLInputTypeAttribute,
    label: string,
    buttonLabel: string,
    onControlSubmit: (value: string) => void
    disabled?: boolean | undefined
}

function SlidoSpammerControl(props: SlidoSpammerControlProps) {
    const {inputType, label, buttonLabel, onControlSubmit, disabled} = props
    const [inputValue, setInputValue] = useState<string>('')

    const onSpamBtnClick = () => {
        onControlSubmit(inputValue)
    }
    
    return (
        <div>
            <label>{label}</label>
            <input type={inputType} value={inputValue} onChange={(e) => setInputValue(e.target.value)}/>
            <button disabled={disabled} onClick={onSpamBtnClick}>{buttonLabel}</button>
        </div>
    )
}

export default SlidoSpammerControl