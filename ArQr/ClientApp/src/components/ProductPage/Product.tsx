import React, {useEffect, useState} from "react";
import {useForm} from "react-hook-form";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faImage, faVideo} from "@fortawesome/free-solid-svg-icons";
import * as Yup from "yup";
import {yupResolver} from "@hookform/resolvers";

import TextInput from "./TextInput";
import ContentInput from "./ContentInput";
import Modal from "../Modal";
import UploadMediaPopup from "./UploadMediaPopup";
import useAuthenticatedHttp from "../hooks/useAuthenticatedHttp";
import {httpStatusCode, urls} from "../services/constants";
import {blobUrlToFile, objectToFormData} from "../services/file";
import {AdsProduct, MediaConfiguration, ProductContentType, UseState} from "../types";
import {Phone} from "../../images";

const Product = () => {
    const initialValues: AdsProduct = {
        title      : "",
        description: "",
        content    : {
            type : "Text",
            value: ""
        }
    };
    const validationSchema = Yup.object(
        {
            title      : Yup.string()
                            .required("عنوان وارد نشده است.")
                            .max(32, "حداکثر 32 کاکتر برای عنوان."),
            description: Yup.string()
                            .required("توضیحات وارد نشده است.")
                            .max(256, "حداکثر 256 کاکتر برای توضیحات."),
            content    : Yup.object(
                {
                    value: Yup.string()
                              .required("محتوا وارد نشده است.")
                              .max(256, "حداکثر 256 کاکتر. برای محتوا")
                }
            )
        }
    );

    const [mediaConfiguration, setMediaConfiguration] = useState<UseState<MediaConfiguration>>(null);
    const [modalVisibility, setModalVisibility] = useState(false);
    const [mediaInfo, setMediaInfo] = useState<UseState<{ file: File, sessionId: string }>>(null);

    const {
        register,
        watch,
        setValue,
        handleSubmit: handleFormSubmit,
        errors
    } = useForm({defaultValues: initialValues, resolver: yupResolver(validationSchema)});
    const {title, description, content} = watch<keyof AdsProduct>(["title", "description", "content"]);
    const {get, post} = useAuthenticatedHttp();

    useEffect(() => {
        fetchConfiguration();
    }, []);

    useEffect(() => {
        console.log(errors);
    }, [errors]);

    const fetchConfiguration = async () => {
        const {status, data: configuration} = await get<MediaConfiguration>(urls.fileManagement.configuration);
        if (status !== httpStatusCode.success) throw Error("Can not fetch configuration");
        setMediaConfiguration(configuration);
    };

    const convertToEnum = (type: ProductContentType) => {
        switch (type) {
            case "Text":
                return 0;
            case "Picture":
                return 1;
            case "Video":
                return 2;
        }
    };
    const convertToServerModel = (product: AdsProduct) => {
        return {
            title      : product.title,
            description: product.description,
            type       : convertToEnum(product.content.type),
            Content    : product.content.value
        };
    };
    const addProduct = async (product: AdsProduct) => {
        const data = convertToServerModel(product);
        const response = await post(urls.apis.product, data);
        if (response.status === httpStatusCode.success) {
            // TODO: Display a notification
        }
    };

    const uploadTextProduct = async (product: AdsProduct) => {
        await addProduct(product);
    };
    const uploadMediaProduct = async (product: AdsProduct) => {
        setModalVisibility(true);
        const file = await blobUrlToFile(product.content.value);
        const form = objectToFormData({fileMime: file.type, fileSize: file.size});
        const {status, statusText, data: sessionId} = await post<string>(urls.fileManagement.createSession, form);
        if (status !== httpStatusCode.success) {
            console.error(statusText);
            return;
        }
        setMediaInfo({file, sessionId});
    };

    const handelSubmit = async (product: AdsProduct) => {
        if (content.type === "Text") await uploadTextProduct(product);
        else await uploadMediaProduct(product);
    };

    const endTheUploadSession = async () => {
        const data = objectToFormData({sessionId: mediaInfo?.sessionId});
        const response = await post(urls.fileManagement.end, data);
        return response.status === httpStatusCode.success;
    };

    const handleUploadMediaCompleted = async () => {
        setModalVisibility(false);
        if (!await endTheUploadSession()) {
            // TODO: Display a notification
            return;
        }
        const product: AdsProduct = {
            title,
            description,
            content: {type: content.type, value: mediaInfo!.sessionId}
        };
        await addProduct(product);
    };
    const handleUploadMediaCanceled = async () => {
        // TODO: Display a notification
        setModalVisibility(false);
        await endTheUploadSession();
    };


    const renderPreview = () => {
        return <div id='preview'>
            <div id='preview-container'>
                <img src={Phone} alt='Phone'/>
                <div id="content">
                    {
                        content.type === "Text" &&
                        <div id="text"><p>{content.value || "متن خود را وارد کنید."}</p></div>
                    }
                    {
                        content.type === "Picture" &&
                        (content.value ?
                         <img src={content.value} alt='content'/> :
                         <FontAwesomeIcon icon={faImage}/>)
                    }
                    {
                        content.type === "Video" &&
                        (content.value ?
                         <video src={content.value} controls autoPlay/> :
                         <FontAwesomeIcon icon={faVideo}/>)
                    }
                </div>
                <div id="detail">
                    <p>{title || "عنوان"}</p>
                    <p>{description || "توضیحات"}</p>
                </div>
            </div>
        </div>;
    };
    const renderForm = () => {
        return <div id='form'>
            <div className="header">
                <h2>محصول جدید</h2>
            </div>
            <form onSubmit={handleFormSubmit(handelSubmit)}>
                <TextInput name='title' register={register} placeholder='عنوان'/>
                <TextInput name='description' register={register} placeholder='توضیحات' lines={5}/>
                <ContentInput register={register}
                              setValue={setValue}
                              pictureMaxSize={mediaConfiguration?.fileSizeLimitInByte.image || 0}
                              videoMaxSize={mediaConfiguration?.fileSizeLimitInByte.video || 0}/>
                <input type="submit" value="ایجاد"/>
            </form>
        </div>;
    };
    const renderUploadPopup = () => {
        return (
            <Modal isVisible={modalVisibility}>
                <UploadMediaPopup file={mediaInfo?.file}
                                  sessionId={mediaInfo?.sessionId}
                                  chunkSize={mediaConfiguration?.chunkSizeInByte}
                                  onUploadCompleted={handleUploadMediaCompleted}
                                  onUploadCanceled={handleUploadMediaCanceled}
                />
            </Modal>
        );
    };
    return (
        <>
            <div id='product'>
                {renderPreview()}
                {renderForm()}
            </div>
            {renderUploadPopup()}
        </>
    );
};

export default Product;