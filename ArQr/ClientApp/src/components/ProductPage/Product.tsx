import React, {useEffect, useState} from "react";
import {useForm} from "react-hook-form";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faImage, faVideo} from "@fortawesome/free-solid-svg-icons";

import TextInput from "./TextInput";
import ContentInput from "./ContentInput";
import {Phone} from "../../images";
import {AdsProduct, MediaConfiguration, UseState} from "../types";
import useAuthenticatedHttp from "../hooks/useAuthenticatedHttp";
import {httpStatusCode, urls} from "../services/constants";
import * as Yup from "yup";
import {yupResolver} from "@hookform/resolvers";

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

    const {
        register,
        watch,
        setValue,
        handleSubmit: handleFormSubmit,
        errors
    } = useForm({defaultValues: initialValues, resolver: yupResolver(validationSchema)});
    const {title, description, content} = watch<keyof AdsProduct>(["title", "description", "content"]);
    const {get} = useAuthenticatedHttp();

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

    const handelSubmit = (product: AdsProduct) => {
        console.log(product);
    };
    return (
        <div id='product'>
            <div id='preview'>
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
            </div>
            <div id='form'>
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
            </div>
        </div>
    );
};

export default Product;