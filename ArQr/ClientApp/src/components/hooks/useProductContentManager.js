import {useReducer} from 'react';
import {PICTURE, TEXT, VIDEO} from '../ProductPage/constants';

const INITIAL_VALUE = {
    markerRight: '0',
    contentType: TEXT,
};

const useProductContentManager = () => {
    const reducer = (state, action) => {
        switch (action.type) {
            case TEXT:
                return INITIAL_VALUE;
            case PICTURE:
                return {
                    markerRight: '10rem',
                    contentType: PICTURE,
                };
            case VIDEO:
                return {
                    markerRight: '20rem',
                    contentType: VIDEO,
                };
            default:
                throw new Error('Invalid Action Parameter.');
        }
    };

    const [{markerRight, contentType}, dispatch] = useReducer(reducer, INITIAL_VALUE);

    const selectContent = (contentType) => dispatch({type: contentType});

    return {
        data: {markerRight, contentType},
        selectContent
    };
};

export default useProductContentManager;