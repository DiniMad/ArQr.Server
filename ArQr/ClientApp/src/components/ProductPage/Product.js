import React, {useContext} from 'react';
import {Field, Form, Formik} from 'formik';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';
import {faImage, faVideo} from '@fortawesome/free-solid-svg-icons';

import withContentManagingContext from '../hocs/WithContentManagingContext';
import TextInput from './TextInput';
import ContentInput from './ContentInput';
import ProductContentTypeContext from '../contexts/ProductContentTypeContext';
import {Phone} from '../../images';

const Product = withContentManagingContext(() => {
    const initialValues = {
        title: '',
        description: '',
        contentText: '',
        content: {
            text: ''
        }
    };

    const handelSubmit = (data) => {
        console.log(data);
    };

    const {data: {isItText, isItPicture, isItVideo, file}} = useContext(ProductContentTypeContext);

    return (
        <Formik initialValues={initialValues} onSubmit={handelSubmit}>
            {({values, handleChange}) =>
                <div id='product'>
                    <div id='preview'>
                        <div id='preview-container'>
                            <img src={Phone} alt='Phone'/>
                            <div id="content">
                                {
                                    isItText &&
                                    <div id="text"><p>{values.content.text || 'متن خود را وارد کنید.'}</p></div>
                                }
                                {
                                    isItPicture &&
                                    (file ?
                                     <img src={file} alt='content'/> :
                                     <FontAwesomeIcon icon={faImage}/>)
                                }
                                {
                                    isItVideo &&
                                    (file ?
                                     <video src={file} controls/> :
                                     <FontAwesomeIcon icon={faVideo}/>)
                                }
                            </div>
                            <div id="detail">
                                <p>{values.title || 'عنوان'}</p>
                                <p>{values.description || 'توضیحات'}</p>
                            </div>
                        </div>
                    </div>
                    <div id='form'>
                        <div className="header">
                            <h2>محصول جدید</h2>
                        </div>
                        <Form>
                            <Field as={TextInput} name='title' placeholder='عنوان'/>
                            <Field as={TextInput} name='description' placeholder='توضیحات' lines={5}/>
                            <ContentInput values={values} handelChange={handleChange}/>
                            <input type="submit" value="ایجاد"/>
                        </Form>
                    </div>
                </div>
            }
        </Formik>
    );
});

export default Product;