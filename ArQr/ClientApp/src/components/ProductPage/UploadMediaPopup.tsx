import React, {useContext, useEffect, useState} from "react";
import Resumable from "resumablejs";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faExclamationTriangle} from "@fortawesome/free-solid-svg-icons";

import {AccessTokenContext} from "../contexts/AccessTokenContext";
import {urls} from "../services/constants";
import "./UploadMediaPopup.scss";

type Props = {
    file?: File,
    sessionId?: string,
    chunkSize?: number,
    onUploadCompleted: () => void;
    onUploadCanceled: () => void;
}
const UploadMediaPopup = ({file, sessionId, chunkSize, onUploadCompleted, onUploadCanceled}: Props) => {
    const isPropsInitialized = file && sessionId && chunkSize;

    const [status, setStatus] = useState<"درحال پردازش" | "بارگزاری">("درحال پردازش");
    const [percentage, setPercentage] = useState(0);

    const [accessToken] = useContext(AccessTokenContext)!;

    const resumable = new Resumable({
                                        chunkSize                    : chunkSize,
                                        forceChunkSize               : true,
                                        simultaneousUploads          : 1,
                                        uploadMethod                 : "PUT",
                                        testChunks                   : false,
                                        fileParameterName            : "file",
                                        chunkNumberParameterName     : "chunkNumber",
                                        chunkSizeParameterName       : "chunkSize",
                                        currentChunkSizeParameterName: "chunkSize",
                                        fileNameParameterName        : "fileName",
                                        totalSizeParameterName       : "totalSize",
                                        headers                      : {Authorization: `Bearer ${accessToken}`}
                                    });

    const handleChunkingComplete = () => {
        setStatus("بارگزاری");
        resumable.upload();
    };
    const handleUploadProgress = (file: any) => {
        const percentage: number = (file.progress() * 100);
        setPercentage(percentage);
    };
    const handleUploadError = () => {
        // TODO: Display a notification
        onUploadCanceled();
    };

    const configResumable = () => {
        resumable.opts.target = urls.fileManagement.upload + sessionId;
        resumable.addFile(file!);
        console.log(resumable.files);
        resumable.on("chunkingComplete", handleChunkingComplete);
        resumable.on("fileProgress", handleUploadProgress);
        resumable.on("fileError", handleUploadError);
        resumable.on("fileSuccess", onUploadCompleted);
    };

    useEffect(() => {
        if (isPropsInitialized && resumable.support) configResumable();
    }, [file, chunkSize]);


    const notSupportedMarkup = () => {
        return (
            <div className="not-supported">
                <div className="not-supported__text">
                    <FontAwesomeIcon icon={faExclamationTriangle} size='3x'/>
                    <h2>مرورگر شما نیاز به بروزرسانی دارد.</h2>
                    <FontAwesomeIcon icon={faExclamationTriangle} size='3x'/>
                </div>
                <div className="not-supported__button">
                    <button onClick={onUploadCanceled}>بستن</button>
                </div>
            </div>
        );
    };
    const progressBarMarkup = () => {
        return (
            <div className="upload">
                <div className="upload__status">
                    <h2>{status}</h2>
                </div>
                <div className="progress-bar">
                    <div className="progress-bar__pointer" style={{width: `${percentage}%`}}/>
                    <div className="progress-bar__percentage">
                        <p>{percentage.toFixed(0)} %</p>
                    </div>
                </div>
            </div>
        );
    };
    return (
        <div className="upload-box">
            {resumable.support ? progressBarMarkup() : notSupportedMarkup()}
        </div>
    );
};

export default UploadMediaPopup;