import {useReducer} from "react";

import {ProductContentType} from "../types";

type State = {
    markerRight: string,
    contentType: ProductContentType,
}
type Action = {
    type: ProductContentType
}
type Reducer = (state: State, action: Action) => State;

const INITIAL_VALUE: State = {
    markerRight: "0",
    contentType: "Text"
};

const useProductContentManager = () => {
    const reducer = (state: State, action: Action): State => {
        switch (action.type) {
            case "Text":
                return INITIAL_VALUE;
            case "Picture":
                return {
                    markerRight: "10rem",
                    contentType: action.type
                };
            case "Video":
                return {
                    markerRight: "20rem",
                    contentType: action.type
                };
            default:
                throw new Error("Invalid Action Parameter.");
        }
    };

    const [{markerRight, contentType}, dispatch] =
        useReducer<Reducer, State>(reducer, INITIAL_VALUE, () => INITIAL_VALUE);

    const selectContent = (contentType: ProductContentType) => dispatch({type: contentType});

    return {
        data: {markerRight, contentType},
        selectContent
    };
};

export default useProductContentManager;