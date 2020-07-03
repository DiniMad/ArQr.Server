import React from 'react';
import PropTypes from 'prop-types';

const TextInput = ({name, register, placeholder, lines = 1}) => {
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

TextInput.propTypes = {
    name: PropTypes.string.isRequired,
    placeholder: PropTypes.string.isRequired,
    register: PropTypes.func.isRequired,
    lines: PropTypes.number
};

export default TextInput;