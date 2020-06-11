import React from 'react';
import PropTypes from 'prop-types';

const IntroductionMobile = ({handleLoginButton, handleRegisterButton, ...className}) => {
    return (
        <div id='introduction' {...className}>
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
                        <span>{'>'}</span>
                    </button>
                    <button onClick={handleLoginButton}>
                        <span>{'<'}</span>
                        <span>ورود</span>
                    </button>
                </div>
            </div>
        </div>
    );
};

IntroductionMobile.propTypes = {
    handleLoginButton: PropTypes.func.isRequired,
    handleRegisterButton: PropTypes.func.isRequired,
    className: PropTypes.string
};

export default IntroductionMobile;