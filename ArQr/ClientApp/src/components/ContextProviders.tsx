import React, {ReactChild} from "react";

import AccessTokenProvider from './contexts/AccessTokenContext';
import UserProvider from './contexts/UserContext';

type props = {
    children: ReactChild;
}
const ContextProviders = ({children}:props) => {
    return (
        <AccessTokenProvider>
            <UserProvider>
                {children}
            </UserProvider>
        </AccessTokenProvider>
    );
};

export default ContextProviders;