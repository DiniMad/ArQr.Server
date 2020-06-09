import React, {useState, useEffect, useRef} from 'react';

import BusinessCard from './BusinessCard';
import LoginForm from './LoginForm';
import RegisterForm from './RegisterForm';
import {Dom} from '../utilities';
import {CardFront, CardBack, Phone} from '../../images';

function Home() {
    const [displayRotaryFront, setDisplayRotaryFront] = useState(true);
    const [formsClasses, setFormsClasses] = useState(null);
    const [firstErrorText, setFirstErrorText] = useState(null);

    const formsElement = useRef(null);
    const markerFrontElement = useRef(null);
    const markerBackElement = useRef(null);

    useEffect(() => {
        if (!(formsElement.current && markerFrontElement.current && markerBackElement.current)) return;
        setFormsElementPosition();
    }, [formsElement, markerFrontElement, markerBackElement, displayRotaryFront]); // Set Forms Element Position

    const setFormsElementPosition = () => {
        setFormsClasses(null);

        setTimeout(() => {
            const phoneHeightByWidthRatio = 2;

            const markerElement = displayRotaryFront ?
                                  markerFrontElement.current :
                                  markerBackElement.current;

            const markerOffset = Dom.getElementOffset(markerElement);
            const phoneWidth = formsElement.current.offsetWidth;
            const phoneHeight = phoneWidth * phoneHeightByWidthRatio;

            formsElement.current.style.transform =
                `translate(${markerOffset.left - phoneWidth * .42}px,
                ${markerOffset.top - phoneHeight / 2.5}px`;

            setFormsClasses('display');
        }, 1000);
    };

    const changeRotary = () => setDisplayRotaryFront(currentValue => !currentValue);
    const onFormError = errors => {
        const errorKeys = Object.keys(errors);
        if (errorKeys.length === 0) {
            setFirstErrorText(null);
            return;
        }
        const firstError = errors[errorKeys[0]];
        setFirstErrorText(firstError);
    };

    const rotaryContainerClasses = displayRotaryFront ? null : 'turn';
    return (
        <main id='home'>
            <div id='card-images-wrapper'>
                <div id='rotary-template' className={rotaryContainerClasses}>
                    <BusinessCard name='front' cardImage={CardFront} markerElement={markerFrontElement}/>
                    <BusinessCard name='back' cardImage={CardBack} markerElement={markerBackElement}/>
                </div>
            </div>
            <div id='forms' ref={formsElement}>
                <img id='forms-img' src={Phone} alt='Phone'/>
                <div id='forms-container' className={formsClasses}>
                    {
                        displayRotaryFront ?
                        <LoginForm onChangeFormButtonClick={changeRotary} onFormError={onFormError}/> :
                        <RegisterForm onChangeFormButtonClick={changeRotary} onFormError={onFormError}/>
                    }
                </div>
                <div id='phone-notification' className={firstErrorText && 'display'}>
                    <h2>
                        {firstErrorText}
                    </h2>
                </div>
            </div>
        </main>
    );
}

export default Home;