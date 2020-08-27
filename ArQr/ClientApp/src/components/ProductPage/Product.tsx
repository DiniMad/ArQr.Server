import React, {useEffect} from "react";
import {useForm} from "react-hook-form";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faImage, faVideo} from "@fortawesome/free-solid-svg-icons";

import TextInput from "./TextInput";
import ContentInput from "./ContentInput";
import {Phone} from "../../images";
import {AdsProduct} from "../types";
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

    const {
        register,
        watch,
        setValue,
        handleSubmit: handleFormSubmit,
        errors
    } = useForm({defaultValues: initialValues, resolver: yupResolver(validationSchema)});
    const {title, description, content} = watch<keyof AdsProduct>(["title", "description", "content"]);

    useEffect(() => {
        console.log(errors);
    }, [errors]);

    const handelSubmit =(product: AdsProduct) => {
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
                    <ContentInput register={register} setValue={setValue}/>
                    <input type="submit" value="ایجاد"/>
                </form>
            </div>
        </div>
    );
};

export default Product;