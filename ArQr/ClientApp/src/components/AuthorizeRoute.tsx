import React, {useContext, useEffect, useState} from "react";
import {Redirect, Route, RouteProps} from "react-router-dom";

import {AccessTokenContext} from "./contexts/AccessTokenContext";
import {queryParameters, urls} from "./services/constants";
import useRefreshToken from "./hooks/useRefreshToken";

const AuthorizeRoute = ({component, path, ...rest}: RouteProps) => {
    if(typeof path !=="string") throw Error("Invalid Argument: path In Component: AuthorizeRoute");
    const link = document.createElement("a");
    link.href = path;
    const returnPath = `${link.pathname}${link.search}${link.hash}`;
    const redirectUrl = `${urls.homePage}?${queryParameters.returnPath}=${encodeURI(returnPath)}`;

    const [isReady, setIsReady] = useState(false);
    const [authenticated, setAuthenticated] = useState(false);

    const [accessToken] = useContext(AccessTokenContext)!;

    const refreshToken = useRefreshToken();

    useEffect(() => {
        if (accessToken) {
            setAuthenticated(true);
            setIsReady(true);
        }
        else {
            refreshToken().then(result => {
                setAuthenticated(result);
                setIsReady(true);
            });
        }
    }, []);

    if (!isReady) return <div/>;
    if (authenticated) return <Route {...rest} component={component}/>;
    return <Route {...rest} component={() => <Redirect to={redirectUrl}/>}/>;
};

export default AuthorizeRoute;