import React from 'react';
import PropTypes from 'prop-types';
import {Formik, Form as FormikForm, Field} from 'formik';
import * as Yup from 'yup';

const Form = ({onChangeFormButtonClick, initialValues, onSubmit, registerMode}) => {
    const validationSchema = Yup.object(
        {
            email: Yup.string().email('ایمیل صحیح نمی باشد.')
                      .required('ایمیل وارد نشده است.'),
            password: Yup.string()
                         .required('پسورد وارد نشده است.')
                         .min(4, 'پسورد نمی تواند کمتر از 4 کارکتر باشد.')
                         .matches(/^[a-zA-Z0-9!@#$&*]*$/, 'کارکتر وارد شده مجاز نمی باشد.'),
            passwordConfirmation: Yup.string()
                                     .required('تکرار پسورد وارد نشده است.')
                                     .oneOf([Yup.ref('password')], 'تکرار پسورد با پسورد مطابقت ندارد.')
        }
    );

    return (
        <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit} regi>
            {({errors}) =>
                <div id='form'>
                    <FormikForm className='form'>
                        <div className='input'>
                            <Field id='email' name='email' type='email'/>
                            <label htmlFor='email'>{errors.email}</label>
                        </div>
                        <div className='input'>
                            <Field id='password' name='password' type='password'/>
                            <label htmlFor='password'>{errors.password}</label>
                        </div>
                        {registerMode &&
                        <div className='input'>
                            <Field id='passwordConfirmation' name='passwordConfirmation' type='password'/>
                            <label htmlFor='passwordConfirmation'>{errors.passwordConfirmation}</label>
                        </div>
                        }
                        <Field name='submit' type='submit' value={registerMode ? 'ایجاد حساب' : 'ورود'}/>
                    </FormikForm>
                    <button onClick={onChangeFormButtonClick} type='button'>
                        {registerMode ? 'ورود به حساب کاربری' : 'ایجاد حساب کاربری'}
                    </button>
                </div>
            }
        </Formik>
    );
};

Form.propTypes = {
    initialValues: PropTypes.object.isRequired,
    onSubmit: PropTypes.func.isRequired,
    onChangeFormButtonClick: PropTypes.func.isRequired,
    registerMode: PropTypes.bool
};

export default Form;