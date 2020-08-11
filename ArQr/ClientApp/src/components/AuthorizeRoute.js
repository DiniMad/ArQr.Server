import React, {useContext} from 'react';
import {Redirect, Route} from 'react-router-dom';

import {AccessToken} from './contexts/AccessToken';
import {queryParameters, urls} from './services/constants';

const AuthorizeRoute = ({component: Component, ...rest}) => {
    const link = document.createElement('a');
    link.href = rest.path;
    const returnPath = `${link.pathname}${link.search}${link.hash}`;
    const redirectUrl = `${urls.homePage}?${queryParameters.returnPath}=${encodeURI(returnPath)}`;

    const [accessToken] = useContext(AccessToken);
    return <Route {...rest}
                  render={(props) => accessToken ? <Component {...props} /> : <Redirect to={redirectUrl}/>}
    />;
};

export default AuthorizeRoute;