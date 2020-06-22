import React from 'react';
import PropTypes from 'prop-types';

const TextInput = ({name, placeholder, value, onChange, lines = 1}) => {
    return (
        <div className="text-input">
            {
                lines > 1 ?
                <textarea value={value}
                          onChange={onChange}
                          name={name}
                          id={name}
                          placeholder={placeholder}
                          rows={lines}
                          required/> :
                <input value={value}
                       onChange={onChange}
                       type="text"
                       name={name}
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

TextInput.propTypes = {
    name: PropTypes.string.isRequired,
    placeholder: PropTypes.string.isRequired,
    value: PropTypes.string.isRequired,
    onChange: PropTypes.func.isRequired,
    lines: PropTypes.number
};

export default TextInput;