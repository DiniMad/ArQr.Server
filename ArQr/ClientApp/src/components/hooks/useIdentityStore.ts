import {useContext} from "react";
import jwtDecode from "jwt-decode";

import {oidc} from "../services/constants";
import {AccessTokenContext} from "../contexts/AccessTokenContext";
import {UserContext} from "../contexts/UserContext";
import {User} from "../types";

type AccessTokenProperties = {
    sub: string
    username: string
    email: string
    emailConfirmed: string
    phoneNumber: string
    phoneNumberConfirmed: string
}
const useIdentityStore = () => {
    const [, setAccessToken] = useContext(AccessTokenContext)!;
    const [, setUser] = useContext(UserContext)!;

    return (accessToken: string, refreshToken: string) => {
        const userWithSub = jwtDecode<AccessTokenProperties>(accessToken);
        const user: User = {
            id                  : userWithSub.sub,
            username            : userWithSub.username,
            email               : userWithSub.email,
            emailConfirmed      : userWithSub.emailConfirmed === "True",
            phoneNumber         : userWithSub.phoneNumber,
            phoneNumberConfirmed: userWithSub.phoneNumberConfirmed === "True"
        };
        setUser(user);
        setAccessToken(accessToken);
        localStorage.setItem(oidc.refresh_token.key, refreshToken);
    };
};

export default useIdentityStore;