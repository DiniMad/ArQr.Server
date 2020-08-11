import {queryParameters, urls} from './constants';

export const getRedirectPath = () => {
    const searchParams = new URLSearchParams(window.location.search);
    return searchParams.get(queryParameters.returnPath) || urls.dashboardPage;
};