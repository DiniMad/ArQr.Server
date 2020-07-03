import React, {useEffect, useRef} from 'react';
import PropTypes from 'prop-types';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';
import {faImage, faVideo} from '@fortawesome/free-solid-svg-icons';

import TextInput from './TextInput';
import useProductContentManager from '../hooks/useProductContentManager';
import {PICTURE, TEXT, VIDEO} from './constants';

const ContentInput = ({register, setValue}) => {
    const inputFileElement = useRef(null);

    const {data: {markerRight, contentType}, selectContent} = useProductContentManager();

    useEffect(() => {
        if (contentType !== TEXT) register({name: 'content.value'});
        register({name: 'content.type'});
        setValue('content.value', '');
        setValue('content.type', contentType);
    }, [contentType]);

    const handleTextButton = () => selectContent(TEXT);
    const handlePictureButton = () => selectContent(PICTURE);
    const handleVideoButton = () => selectContent(VIDEO);

    const openFileDialog = accept => {
        const inputFile = inputFileElement.current;
        if (!inputFile) return;
        inputFile.accept = accept;
        inputFile.click();
    };

    const handelSelectPictureButton = () => openFileDialog('.png,.jpg,.jpeg');
    const handelSelectVideoButton = () => openFileDialog('.mp4');
    const handleInputFileChange = async () => {
        const inputFile = inputFileElement.current;
        const file = inputFileElement.current.files[0];
        if (!(inputFile && file)) return;

        const media = URL.createObjectURL(file);
        setValue('content.value', media);
        setValue('content.type', contentType);

        inputFile.value = '';
    };

    return (
        <div id='product-content'>
            <div id='product-content-header'>
                <div id='product-content-header-marker' style={{right: markerRight}}/>
                <button onClick={handleTextButton} type='button' className='header-item'><p>متن</p></button>
                <button onClick={handlePictureButton} type='button' className='header-item'><p>تصویر</p></button>
                <button onClick={handleVideoButton} type='button' className='header-item'><p>ویدیو</p></button>
            </div>
            <div id='product-content-input'>
                <input ref={inputFileElement} onChange={handleInputFileChange} type='file' name='file-input'
                       id='input'/>
                {
                    contentType === TEXT &&
                    <TextInput name='content.value' register={register} placeholder='متن' lines={4}/>
                }
                {
                    contentType === PICTURE &&
                    <button onClick={handelSelectPictureButton} type='button'>
                        <FontAwesomeIcon icon={faImage}/>
                    </button>
                }
                {
                    contentType === VIDEO &&
                    <button onClick={handelSelectVideoButton} type='button'>
                        <FontAwesomeIcon icon={faVideo}/>
                    </button>
                }
            </div>
        </div>
    );
};

ContentInput.propTypes = {
    register: PropTypes.func.isRequired,
    setValue: PropTypes.func.isRequired
};

export default ContentInput;