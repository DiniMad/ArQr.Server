import React, {useContext, useEffect, useRef, useState} from 'react';
import {Field} from 'formik';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';
import {faImage, faVideo} from '@fortawesome/free-solid-svg-icons';

import TextInput from './TextInput';
import ProductContentTypeContext from '../contexts/ProductContentTypeContext';

const ContentInput = () => {
    const [file, setFile] = useState({type: null, file: null});

    const fileInputElement = useRef(null);

    useEffect(() => {
        if (!file.type) return;
        selectContent(file.type, file.file);
    }, [file]);

    const {
        data: {markerRight, isItText, isItPicture, isItVideo},
        constant: {TEXT, PICTURE, VIDEO},
        selectContent
    } = useContext(ProductContentTypeContext);

    const handleTextButton = () => selectContent(TEXT);
    const handlePictureButton = () => selectContent(PICTURE);
    const handleVideoButton = () => selectContent(VIDEO);

    const openFileDialog = accept => {
        const fileInput = fileInputElement.current;
        if (!fileInput) return;
        fileInput.accept = accept;
        fileInput.click();
    };
    const readFile = async image => {
        if (!image) return;

        const type = image.type.startsWith('image') ? PICTURE : VIDEO;
        const reader = new FileReader();
        reader.onload = async e => setFile({type, file: await e.target.result});
        await reader.readAsDataURL(image);
    };

    const handelSelectPictureButton = () => openFileDialog('.png,.jpg,.jpeg');
    const handelSelectVideoButton = () => openFileDialog('.mp4');
    const handleInputFileChange = async () => {
        const fileInput = fileInputElement.current;
        if (!fileInput || fileInput.files.length === 0) return;
        await readFile(fileInput.files[0]);
        fileInput.value = '';
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
                <input ref={fileInputElement} onChange={handleInputFileChange} type='file' name='file-input'
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