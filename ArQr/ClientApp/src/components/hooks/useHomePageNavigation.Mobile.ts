import {useReducer} from "react";

type State = {
    introductionClasses: string | null,
    containerClasses: string | null,
    isItLoginPage: boolean,
    isItRegisterPage: boolean
}
type ActionTypes = "LoginPage" | "RegisterPage";
type Action = {
    type: ActionTypes
}
type Reducer = (state: State, action: Action) => State

const initialValue: State = {
    introductionClasses: null,
    containerClasses   : null,
    isItLoginPage      : false,
    isItRegisterPage   : false
};

function useHomePageNavigationMobile() {
    const reducer: Reducer = (state: State, action: Action) => {
        switch (action.type) {
            case "LoginPage":
                return {
                    introductionClasses: "hide",
                    containerClasses   : null,
                    isItLoginPage      : true,
                    isItRegisterPage   : false
                };
            case "RegisterPage":
                return {
                    introductionClasses: "hide",
                    containerClasses   : "no-margin",
                    isItLoginPage      : false,
                    isItRegisterPage   : true
                };
            default:
                throw new Error("Invalid Action Parameter.");
        }
    };

    const [data, dispatch] = useReducer(reducer, initialValue);

    const navigateTo = (page: ActionTypes) => dispatch({type: page});

    const returnData: {
        data: State,
        actionTypes: ActionTypes[],
        navigateTo: (page: ActionTypes) => void
    } = {
        data       : data,
        actionTypes: ["LoginPage", "RegisterPage"],
        navigateTo
    };
    return returnData;
}

export default useHomePageNavigationMobile;