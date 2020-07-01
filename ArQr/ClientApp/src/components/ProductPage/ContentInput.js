import React, {useContext, useRef} from 'react';
import {Field} from 'formik';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';
import {faImage, faVideo} from '@fortawesome/free-solid-svg-icons';

import TextInput from './TextInput';
import ProductContentTypeContext from '../contexts/ProductContentTypeContext';

const ContentInput = () => {
    const inputFileElement = useRef(null);

    const {
        data: {markerRight, isItText, isItPicture, isItVideo},
        constant: {TEXT, PICTURE, VIDEO},
        selectContent
    } = useContext(ProductContentTypeContext);

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
        const inputFile = inputFileElement.current && inputFileElement.current.files[0];
        if (!inputFile) return;

        const type = inputFile.type.startsWith('image') ? PICTURE : VIDEO;
        const file = URL.createObjectURL(inputFile);
        selectContent(type, file);
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
                    isItText &&
                    <Field as={TextInput} name='content.text' placeholder='متن' lines={4}/>
                }
                {
                    isItPicture &&
                    <button onClick={handelSelectPictureButton} type='button'>
                        <FontAwesomeIcon icon={faImage}/>
                    </button>
                }
                {
                    isItVideo &&
                    <button onClick={handelSelectVideoButton} type='button'>
                        <FontAwesomeIcon icon={faVideo}/>
                    </button>
                }
            </div>
        </div>
    );
};

export default ContentInput;