import React, {useState, useEffect, useRef} from 'react';

import BusinessCard from './BusinessCard';
import LoginForm from './LoginForm';
import RegisterForm from './RegisterForm';
import {Dom} from '../utilities';
import {CardFront, CardBack, Phone} from '../../images';

function Home() {
    const [displayRotaryFront, setDisplayRotaryFront] = useState(true);
    const [formsClasses, setFormsClasses] = useState(null);

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
            const phoneHeightByWidthRatio = .5;

            const markerElement = displayRotaryFront ?
                                  markerFrontElement.current :
                                  markerBackElement.current;

            const markerOffset = Dom.getElementOffset(markerElement);
            const phoneWidth = formsElement.current.offsetWidth;
            const phoneHeight = phoneWidth * phoneHeightByWidthRatio;

            formsElement.current.style.transform =
                `translate(${markerOffset.left - phoneWidth * .42}px,
                ${markerOffset.top - phoneHeight / 2}px`;

            setFormsClasses('display');
        }, 1000);
    };

    const changeRotary = () => setDisplayRotaryFront(currentValue => !currentValue);

    const rotaryContainerClasses = displayRotaryFront ? null : 'turn';
    return (
        <main id='home'>
            <div id="card-images-wrapper">
                <div id="rotary-template" className={rotaryContainerClasses}>
                    <BusinessCard name='front' cardImage={CardFront} markerElement={markerFrontElement}/>
                    <BusinessCard name='back' cardImage={CardBack} markerElement={markerBackElement}/>
                </div>
            </div>
            <div id="forms" ref={formsElement}>
                <img id="forms-img" src={Phone} alt="Phone"/>
                <div id="forms-container" className={formsClasses}>
                    {
                        displayRotaryFront ?
                        <LoginForm onChangeFormButtonClick={changeRotary}/> :
                        <RegisterForm onChangeFormButtonClick={changeRotary}/>
                    }
                </div>
            </div>
        </main>
    );
}

export default Home;