import React, {ReactChild, useState} from "react";

import {CreateContextForUseState, UseState} from "../types";

export const AccessTokenContext = React.createContext<CreateContextForUseState<string>>(null);

type Props = {
    children: ReactChild
}
const AccessTokenProvider = ({children}: Props) => {
    const [accessToken, setAccessToken] = useState<UseState<string>>(null);
    return (
        <AccessTokenContext.Provider value={[accessToken, setAccessToken]}>
            {children}
        </AccessTokenContext.Provider>
    );
};

export default AccessTokenProvider;