import React, {useState} from 'react';

export const AccessTokenContext = React.createContext(null);

const AccessTokenProvider = ({children}) => {
    const [accessToken, setAccessToken] = useState(null);

    return (
        <AccessTokenContext.Provider value={[accessToken, setAccessToken]}>
            {children}
        </AccessTokenContext.Provider>
    );
};

export default AccessTokenProvider;