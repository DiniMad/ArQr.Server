import React, {useState} from 'react';

export const UserContext = React.createContext(null);

const UserProvider = ({children}) => {
    const [user, setAccessToken] = useState(null);

    return (
        <UserContext.Provider value={[user, setAccessToken]}>
            {children}
        </UserContext.Provider>
    );
};

export default UserProvider;