import React, {MouseEventHandler, useEffect} from "react";
import {useForm} from "react-hook-form";
import {yupResolver} from "@hookform/resolvers";
import * as Yup from "yup";

import {UserIdentity} from "../types";

type Props = {
    onSubmit: (data: UserIdentity) => void,
    onFormError: (error: string | null) => void,
    onChangeFormButtonClick: MouseEventHandler,
    registerMode?: boolean
}
const Form = ({onSubmit, onFormError, onChangeFormButtonClick, registerMode}: Props) => {
    const initialValues: {
        email: string,
        password: string,
        passwordConfirmation?: string
    } = {
        email               : "",
        password            : "",
        passwordConfirmation: undefined
    };

    let passwordConfirmation: Yup.StringSchema;
    if (registerMode) {
        initialValues.passwordConfirmation = "";
        passwordConfirmation = Yup.string()
                                  .required("تکرار پسورد وارد نشده است.")
                                  .oneOf([Yup.ref("password")], "تکرار پسورد با پسورد مطابقت ندارد.");
    }


    const validationSchema = Yup.object(
        {
            email               : Yup.string().email("ایمیل صحیح نمی باشد.")
                                     .required("ایمیل وارد نشده است."),
            password            : Yup.string()
                                     .required("پسورد وارد نشده است.")
                                     .min(6, "پسورد نمی تواند کمتر از 6 کارکتر باشد.")
                                     .matches(/\d/, "پسورد باید شامل حداقل یک عدد باشد.")
                                     .matches(/^[a-zA-Z0-9!@#$&*]*$/, "کارکتر وارد شده در پسورد مجاز نمی باشد."),
            passwordConfirmation: Yup.string().concat(passwordConfirmation!)
        }
    );

    const {register, handleSubmit, errors} = useForm({
                                                         mode         : "onBlur",
                                                         defaultValues: initialValues,
                                                         resolver     : yupResolver(validationSchema)
                                                     });

    useEffect(() => {
        errors.email && errors.email.message ? onFormError(errors.email.message) :
        errors.password && errors.password.message ? onFormError(errors.password.message) :
        errors.passwordConfirmation && errors.passwordConfirmation.message ?
        onFormError(errors.passwordConfirmation.message) :
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
                        <input name='passwordConfirmation'
                               ref={register} type='password'
                               placeholder='تکرار رمز عبور'/>
                    }
                </div>
                <input name='submit' type='submit' value={registerMode ? "ایجاد حساب" : "ورود"}/>
            </form>
            <button onClick={onChangeFormButtonClick} type='button'>
                {registerMode ? "ورود به حساب" : "ایجاد حساب"}
            </button>
        </>
    );
};

export default Form;