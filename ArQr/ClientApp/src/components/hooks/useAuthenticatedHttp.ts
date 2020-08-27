import Axios from "axios";
import {useContext} from "react";

import {AccessTokenContext} from "../contexts/AccessTokenContext";

const useAuthenticatedHttp = () => {
    const [accessToken] = useContext(AccessTokenContext)!;
    Axios.defaults.headers = {Authorization: `Bearer ${accessToken}`};

    return {
        get : Axios.get,
        post: Axios.post
    };
};

export default useAuthenticatedHttp;