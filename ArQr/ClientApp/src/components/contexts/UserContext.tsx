import React, {ReactChild, useState} from "react";

import {CreateContextForUseState, User, UseState} from "../types";

export const UserContext = React.createContext<CreateContextForUseState<User>>(null);

type Props = {
    children: ReactChild
}
const UserProvider = ({children}: Props) => {
    const [user, setAccessToken] = useState<UseState<User>>(null);

    return (
        <UserContext.Provider value={[user, setAccessToken]}>
            {children}
        </UserContext.Provider>
    );
};

export default UserProvider;