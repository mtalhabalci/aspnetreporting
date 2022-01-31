export const checkAuthentication = (state) => {
    return state.isAuthenticated;
};

export const getUser = (state) => {
    return state.user;
};

export const getJwt = (state) => {
    return state.jwt;
};
export const getPrivileges = (state) => {
    return state.privileges;
};

export const getApplicationInformationIsLoaded = (state) => {
    return state.applicationInformationIsLoaded;
};

export const getPrivilegeNames = (state) => {
    return state.privilegeNames;
};


export const getContainerLoader = (state) => {
    return state.containerLoader;
};
