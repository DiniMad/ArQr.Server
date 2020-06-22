import {useReducer} from 'react';

const TEXT = 'TEXT', PICTURE = 'PICTURE', VIDEO = 'VIDEO';
const INITIAL_VALUE = {
    markerRight: '0',
    isItText: true,
    isItPicture: false,
    isItVideo: false,
    file: null
};

const UseProductContentManager = () => {
    const reducer = (state, action) => {
        switch (action.type) {
            case TEXT:
                return INITIAL_VALUE;
            case PICTURE:
                return {
                    markerRight: '10rem',
                    isItText: false,
                    isItPicture: true,
                    isItVideo: false,
                    file: action.payload.file
                };
            case VIDEO:
                return {
                    markerRight: '20rem',
                    isItText: false,
                    isItPicture: false,
                    isItVideo: true,
                    file: action.payload.file
                };
            default:
                throw new Error('Invalid Action Parameter.');
        }
    };

    const [{markerRight, isItText, isItPicture, isItVideo, file}, dispatch] = useReducer(reducer, INITIAL_VALUE);

    const selectContent = (contentType, file = null) => dispatch({type: contentType, payload: {file}});

    return {
        data: {markerRight, isItText, isItPicture, isItVideo, file},
        constant: {TEXT, PICTURE, VIDEO},
        selectContent
    };
};

export default UseProductContentManager;