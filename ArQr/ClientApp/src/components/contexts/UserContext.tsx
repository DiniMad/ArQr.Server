import React, {ReactChild, useState} from "react";

import {CreateContextForUseState, UserType, UseState} from "../types";

export const UserContext = React.createContext<CreateContextForUseState<UserType>>(null);

type props = {
    children: ReactChild
}
const UserProvider = ({children}: props) => {
    const [user, setAccessToken] = useState<UseState<UserType>>(null);

    return (
        <UserContext.Provider value={[user, setAccessToken]}>
            {children}
        </UserContext.Provider>
    );
};

export default UserProvider;