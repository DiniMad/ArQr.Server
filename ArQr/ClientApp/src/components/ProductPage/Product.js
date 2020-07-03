import React from 'react';
import {useForm} from 'react-hook-form';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';
import {faImage, faVideo} from '@fortawesome/free-solid-svg-icons';

import TextInput from './TextInput';
import ContentInput from './ContentInput';
import {Phone} from '../../images';
import {PICTURE, TEXT, VIDEO} from './constants';

const Product = () => {
    const initialValues = {
        title: '',
        description: '',
        content: {
            type: TEXT,
            value: ''
        }
    };

    const {register, watch, setValue, handleSubmit: handleFormSubmit} = useForm({defaultValues: initialValues});
    const {title, description, content} = watch(['title', 'description', 'content']);

    const handelSubmit = (data) => {
        console.log(data);
    };

    return (
        <div id='product'>
            <div id='preview'>
                <div id='preview-container'>
                    <img src={Phone} alt='Phone'/>
                    <div id="content">
                        {
                            content.type === TEXT &&
                            <div id="text"><p>{content.value || 'متن خود را وارد کنید.'}</p></div>
                        }
                        {
                            content.type === PICTURE &&
                            (content.value ?
                             <img src={content.value} alt='content'/> :
                             <FontAwesomeIcon icon={faImage}/>)
                        }
                        {
                            content.type === VIDEO &&
                            (content.value ?
                             <video src={content.value} controls/> :
                             <FontAwesomeIcon icon={faVideo}/>)
                        }
                    </div>
                    <div id="detail">
                        <p>{title || 'عنوان'}</p>
                        <p>{description || 'توضیحات'}</p>
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
                    <ContentInput register={register} setValue={setValue} contentType={content.type}/>
                    <input type="submit" value="ایجاد"/>
                </form>
            </div>
        </div>
    );
};

export default Product;