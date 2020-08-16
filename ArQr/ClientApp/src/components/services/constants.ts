export const urls = {
    homePage: '/',
    dashboardPage: '/dashboard',
    tokenEndPoint: 'connect/token',
    registerEndPoint: 'account/register'
};

export const oidc = {
    client_id: {key: 'client_id', value: 'ArQr'},
    grant_type: {key: 'grant_type', password: 'password', refreshToken: 'refresh_token'},
    scope: {key: 'scope', value: 'ArQrAPI offline_access'},
    refresh_token: {key: 'refresh_token'}
};

export const httpStatusCode = {
    success: 200,
    created: 201
};

export const queryParameters = {
    returnPath: 'returnPath'
};