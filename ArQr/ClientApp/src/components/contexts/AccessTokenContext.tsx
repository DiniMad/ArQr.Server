import React, {ReactChild, useState} from "react";

import {createContextForUseStateType, useStateType} from "../types";

export const AccessTokenContext = React.createContext<createContextForUseStateType<string>>(null);

type props = {
    children: ReactChild
}
const AccessTokenProvider = ({children}: props) => {
    const [accessToken, setAccessToken] = useState<useStateType<string>>(null);
    return (
        <AccessTokenContext.Provider value={[accessToken, setAccessToken]}>
            {children}
        </AccessTokenContext.Provider>
    );
};

export default AccessTokenProvider;