import React, {ReactChild, useState} from "react";

import {CreateContextForUseStateType, UseStateType} from "../types";

export const AccessTokenContext = React.createContext<CreateContextForUseStateType<string>>(null);

type props = {
    children: ReactChild
}
const AccessTokenProvider = ({children}: props) => {
    const [accessToken, setAccessToken] = useState<UseStateType<string>>(null);
    return (
        <AccessTokenContext.Provider value={[accessToken, setAccessToken]}>
            {children}
        </AccessTokenContext.Provider>
    );
};

export default AccessTokenProvider;