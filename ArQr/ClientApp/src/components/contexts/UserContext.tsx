import React, {ReactChild, useState} from "react";

import {CreateContextForUseState, User, UseState} from "../types";

export const UserContext = React.createContext<CreateContextForUseState<User>>(null);

type props = {
    children: ReactChild
}
const UserProvider = ({children}: props) => {
    const [user, setAccessToken] = useState<UseState<User>>(null);

    return (
        <UserContext.Provider value={[user, setAccessToken]}>
            {children}
        </UserContext.Provider>
    );
};

export default UserProvider;