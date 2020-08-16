import React from "react";

type Props = {
    handleLoginButton: () => void,
    handleRegisterButton: () => void,
    className: string | undefined
}
const IntroductionMobile = ({handleLoginButton, handleRegisterButton, className}: Props) => {
    return (
        <div id='introduction' className={className}>
            <div id="introduction-container">
                <div id="text">
                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Adipisci nam quis quo veniam
                       voluptates.
                       Accusamus accusantium adipisci amet animi asperiores beatae debitis deleniti dolores dolorum,
                       earum
                       enim error ex id impedit ipsam maxime modi mollitia nam nesciunt nostrum perferendis quasi quos,
                       rem
                       repellat repudiandae sint sit ullam velit voluptas voluptatem?</p>
                </div>
                <div id="navigation">
                    <button onClick={handleRegisterButton}>
                        <span>ایجاد حساب</span>
                        <span>{">"}</span>
                    </button>
                    <button onClick={handleLoginButton}>
                        <span>{"<"}</span>
                        <span>ورود</span>
                    </button>
                </div>
            </div>
        </div>
    );
};

export default IntroductionMobile;