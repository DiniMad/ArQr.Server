import React, {useState, useEffect, useRef} from "react";

import BusinessCard from "./BusinessCard";
import LoginForm from "./LoginForm";
import RegisterForm from "./RegisterForm";
import Notification from "../Notification";
import {Dom} from "../utilities";
import {CardFront, CardBack, Phone} from "../../images";
import useSilentLogin from "../hooks/useSilentLogin";
import {UseState} from "../types";

function Home() {
    const [displayRotaryFront, setDisplayRotaryFront] = useState<UseState<boolean>>(true);
    const [formsClasses, setFormsClasses] = useState<UseState<string>>(null);
    const [errorText, setErrorText] = useState<UseState<string>>(null);

    const formsElement = useRef<HTMLDivElement>(null);
    const markerFrontElement = useRef<HTMLDivElement>(null);
    const markerBackElement = useRef<HTMLDivElement>(null);

    useSilentLogin();

    useEffect(() => {
        if (!(formsElement.current && markerFrontElement.current && markerBackElement.current)) return;
        setFormsElementPosition();
    }, [formsElement, markerFrontElement, markerBackElement, displayRotaryFront]); // Set Forms Element Position

    const setFormsElementPosition = () => {
        setFormsClasses(null);

        setTimeout(() => {
            if (!formsElement.current || !markerFrontElement.current || !markerBackElement.current) return;

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

            setFormsClasses("display");
        }, 1000);
    };

    const changeRotary = () => setDisplayRotaryFront(currentValue => !currentValue);
    const onFormError = (error: string|null) => setErrorText(error);

    const rotaryContainerClasses = displayRotaryFront ? undefined : "turn";
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
                <div id='forms-container' className={formsClasses||undefined}>
                    {
                        displayRotaryFront ?
                        <LoginForm onChangeFormButtonClick={changeRotary} onFormError={onFormError}/> :
                        <RegisterForm onChangeFormButtonClick={changeRotary} onFormError={onFormError}/>
                    }
                </div>
                <Notification text={errorText}/>
            </div>
        </main>
    );
}

export default Home;