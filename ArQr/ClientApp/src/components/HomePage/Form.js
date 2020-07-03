import React, {useEffect} from 'react';
import PropTypes from 'prop-types';
import {useForm} from 'react-hook-form';
import {yupResolver} from '@hookform/resolvers';
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


    const {register, handleSubmit, errors} = useForm({
                                                         mode: 'onBlur',
                                                         defaultValues: initialValues,
                                                         resolver: yupResolver(validationSchema)
                                                     });

    useEffect(() => {
        errors.email ? onFormError(errors.email.message) :
        errors.password ? onFormError(errors.password.message) :
        errors.passwordConfirmation ? onFormError(errors.passwordConfirmation.message) :
        onFormError(null);
    }, [Object.values(errors)]);

    return (
        <>
            <form onSubmit={handleSubmit(onSubmit)} className='form'>
                <div className='inputs'>
                    <input name='email' ref={register} type='email' placeholder='ایمیل'/>
                    <input name='password' ref={register} type='password' placeholder='رمز عبور'/>
                    {
                        registerMode &&
                        <input name='passwordConfirmation' ref={register} type='password' placeholder='تکرار رمز عبور'/>
                    }
                </div>
                <input name='submit' type='submit' value={registerMode ? 'ایجاد حساب' : 'ورود'}/>
            </form>
            <button onClick={onChangeFormButtonClick} type='button'>
                {registerMode ? 'ورود به حساب' : 'ایجاد حساب'}
            </button>
        </>
    );
};

Form.propTypes = {
    onSubmit: PropTypes.func.isRequired,
    onFormError: PropTypes.func.isRequired,
    onChangeFormButtonClick: PropTypes.func.isRequired,
    registerMode: PropTypes.bool
};

export default Form;