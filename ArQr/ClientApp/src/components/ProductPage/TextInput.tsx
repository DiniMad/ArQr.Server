import React from "react";

type Props = {
    name: string,
    register: (...args: any[]) => any,
    placeholder: string,
    lines?: number
}
const TextInput = ({name, register, placeholder, lines = 1}: Props) => {
    return (
        <div className="text-input">
            {
                lines > 1 ?
                <textarea name={name}
                          ref={register}
                          placeholder={placeholder}
                          rows={lines}
                          required/> :
                <input type="text"
                       name={name}
                       ref={register}
                       id={name}
                       placeholder={placeholder}
                       required/>
            }
            <div className="text-input-marker">
                <div/>
            </div>
        </div>
    );
};

export default TextInput;