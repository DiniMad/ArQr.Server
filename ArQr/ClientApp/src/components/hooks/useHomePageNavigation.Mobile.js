import {useReducer} from 'react';

const LOGIN_Page = 'LOGIN_Page';
const REGISTER_PAGE = 'REGISTER_PAGE';
const initialValue = {
    introductionClasses: null,
    containerClasses: null,
    isItLoginPage: false,
    isItRegisterPage: false
};

function useHomePageNavigationMobile() {
    const reducer = (state, action) => {
        switch (action.type) {
            case LOGIN_Page:
                return {
                    introductionClasses: 'hide',
                    containerClasses: null,
                    isItLoginPage: true,
                    isItRegisterPage: false
                };
            case REGISTER_PAGE:
                return {
                    introductionClasses: 'hide',
                    containerClasses: 'no-margin',
                    isItLoginPage: false,
                    isItRegisterPage: true
                };
            default:
                throw new Error('Invalid Action Parameter.');
        }
    };

    const [{introductionClasses, containerClasses, isItLoginPage, isItRegisterPage}, dispatch] =
        useReducer(reducer, initialValue);

    const navigateTo = (page) => dispatch({type: page});

    return {
        data: {introductionClasses, containerClasses, isItLoginPage, isItRegisterPage},
        constant: {LOGIN_Page, REGISTER_PAGE},
        navigateTo
    };
}

export default useHomePageNavigationMobile;