import React, {useState} from 'react';

export const AccessToken = React.createContext(null);

const AccessTokenProvider = ({children}) => {
    const [accessToken, setAccessToken] = useState(null);

    return (
        <AccessToken.Provider value={[accessToken, setAccessToken]}>
            {children}
        </AccessToken.Provider>
    );
};

export default AccessTokenProvider;