import React, {ReactChild, useState} from "react";

import {CreateContextForUseState, UseState} from "../types";

export const AccessTokenContext = React.createContext<CreateContextForUseState<string>>(null);

type props = {
    children: ReactChild
}
const AccessTokenProvider = ({children}: props) => {
    const [accessToken, setAccessToken] = useState<UseState<string>>(null);
    return (
        <AccessTokenContext.Provider value={[accessToken, setAccessToken]}>
            {children}
        </AccessTokenContext.Provider>
    );
};

export default AccessTokenProvider;