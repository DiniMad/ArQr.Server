import React, {ReactChild} from "react";
import {ToastProvider} from "react-toast-notifications";

import AccessTokenProvider from "./contexts/AccessTokenContext";
import UserProvider from "./contexts/UserContext";

type Props = {
    children: ReactChild;
}
const ContextProviders = ({children}: Props) => {
    return (
        <AccessTokenProvider>
            <UserProvider>
                <ToastProvider autoDismiss autoDismissTimeout={5000}>
                    {children}
                </ToastProvider>
            </UserProvider>
        </AccessTokenProvider>
    );
};

export default ContextProviders;