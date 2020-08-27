import React, {useEffect, useRef} from "react";
import PropTypes from "prop-types";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faImage, faVideo} from "@fortawesome/free-solid-svg-icons";

import TextInput from "./TextInput";
import useProductContentManager from "../hooks/useProductContentManager";

const MB_COEFFICIENT = 1024 * 1024;

type Props = {
    register: (...args: any[]) => any,
    setValue: (...args: any[]) => any,
    pictureMaxSize: number,
    videoMaxSize: number
}
const ContentInput = ({register, setValue, pictureMaxSize, videoMaxSize}: Props) => {
    const inputFileElement = useRef<HTMLInputElement>(null);

    const {data: {markerRight, contentType}, selectContent} = useProductContentManager();

    useEffect(() => {
        if (contentType !== "Text") register({name: "content.value"});
        register({name: "content.type"});
        setValue("content.value", "");
        setValue("content.type", contentType);
    }, [contentType]);

    const handleTextButton = () => selectContent("Text");
    const handlePictureButton = () => selectContent("Picture");
    const handleVideoButton = () => selectContent("Video");

    const openFileDialog = (accept: any) => {
        const inputFile = inputFileElement.current;
        if (!inputFile) return;
        inputFile.accept = accept;
        inputFile.click();
    };

    const handelSelectPictureButton = () => openFileDialog(".png,.jpg,.jpeg");
    const handelSelectVideoButton = () => openFileDialog(".mp4");
    const handleInputFileChange = async () => {
        const inputFile = inputFileElement.current;
        if (!inputFile || !inputFile.files || inputFile.files.length < 1) return;
        const file = inputFile.files[0];

        if (contentType === "Picture" && file.size > pictureMaxSize) {
            console.log(`Maximum size of picture is ${pictureMaxSize / MB_COEFFICIENT} MB`);
            return;
        }
        if (contentType === "Video" && file.size > videoMaxSize) {
            console.log(`Maximum size of video is ${videoMaxSize / MB_COEFFICIENT} MB`);
            return;
        }

        const media = URL.createObjectURL(file);
        setValue("content.value", media);
        setValue("content.type", contentType);

        inputFile.value = "";
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
                    contentType === "Text" &&
                    <TextInput name='content.value' register={register} placeholder='متن' lines={4}/>
                }
                {
                    contentType === "Picture" &&
                    <button onClick={handelSelectPictureButton} type='button'>
                        <FontAwesomeIcon icon={faImage}/>
                    </button>
                }
                {
                    contentType === "Video" &&
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