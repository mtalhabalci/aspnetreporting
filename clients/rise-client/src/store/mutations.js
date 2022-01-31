export const changeAuthentication = (state, payload) => {
    state.isAuthenticated = payload;
};

export const setJwt = (state, payload) => {
    state.jwt = payload;
};

export const setUser = (state, payload) => {
    state.user = payload;
};

export const setPrivileges = (state, payload) => {
    state.privileges = payload;
};

export const setApplicationInformationIsLoaded = (state, payload) => {
    state.applicationInformationIsLoaded = payload;
};

export const setPrivilegeNames = (state) => {
    if(state.privileges){
        var privilegeNames = {};
        state.privileges.forEach(element => {
            privilegeNames[element.name] = element.name;
        });
        state.privilegeNames = privilegeNames;
    }
};


export const setContainerLoader = (state, payload) => {
    state.containerLoader = payload;
};
