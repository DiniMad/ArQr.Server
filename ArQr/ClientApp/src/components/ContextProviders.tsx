import React, {ReactChild} from "react";

import AccessTokenProvider from './contexts/AccessTokenContext';
import UserProvider from './contexts/UserContext';

type Props = {
    children: ReactChild;
}
const ContextProviders = ({children}:Props) => {
    return (
        <AccessTokenProvider>
            <UserProvider>
                {children}
            </UserProvider>
        </AccessTokenProvider>
    );
};

export default ContextProviders;