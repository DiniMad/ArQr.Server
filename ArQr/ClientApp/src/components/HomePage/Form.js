import React from 'react';
import PropTypes from 'prop-types';
import {Formik, Form as FormikForm, Field} from 'formik';
import * as Yup from 'yup';

const Form = ({onSubmit, onFormError, onChangeFormButtonClick, registerMode}) => {
    const initialValues = {
        email: '',
        password: ''
    };

    let passwordConfirmation = null;
    if (registerMode) {
        initialValues.passwordConfirmation = '';
        passwordConfirmation = Yup.string()
                                  .required('تکرار پسورد وارد نشده است.')
                                  .oneOf([Yup.ref('password')], 'تکرار پسورد با پسورد مطابقت ندارد.');
    }


    const validationSchema = Yup.object(
        {
            email: Yup.string().email('ایمیل صحیح نمی باشد.')
                      .required('ایمیل وارد نشده است.'),
            password: Yup.string()
                         .required('پسورد وارد نشده است.')
                         .min(4, 'پسورد نمی تواند کمتر از 4 کارکتر باشد.')
                         .matches(/^[a-zA-Z0-9!@#$&*]*$/, 'کارکتر وارد شده در پسورد مجاز نمی باشد.'),
            passwordConfirmation: Yup.string().concat(passwordConfirmation),
        }
    );


    return (
        <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit} regi>
            {({errors}) => {
                onFormError(errors);

                return (
                    <>
                        <FormikForm className='form'>
                            <div className='inputs'>
                                <Field name='email' type='email' placeholder='ایمیل'/>
                                <Field name='password' type='password' placeholder='رمز عبور'/>
                                {registerMode &&
                                <Field name='passwordConfirmation'
                                       type='password'
                                       placeholder='تکرار رمز عبور'/>
                                }
                            </div>
                            <Field name='submit' type='submit' value={registerMode ? 'ایجاد حساب' : 'ورود'}/>
                        </FormikForm>
                        <button onClick={onChangeFormButtonClick} type='button'>
                            {registerMode ? 'ورود به حساب' : 'ایجاد حساب'}
                        </button>
                    </>
                );
            }
            }
        </Formik>
    );
};

Form.propTypes = {
    onSubmit: PropTypes.func.isRequired,
    onFormError: PropTypes.func.isRequired,
    onChangeFormButtonClick: PropTypes.func.isRequired,
    registerMode: PropTypes.bool
};

export default Form;