export const changeAuthentication = ({ commit }, payload) => {
    commit("changeAuthentication", payload);
};

export const setJwt = ({ commit }, payload) => {
    commit("setJwt", payload);
};

export const setUser = ({ commit }, payload) => {
    commit("setUser", payload);
};

export const setPrivileges = ({ commit }, payload) => {
    commit("setPrivileges", payload);
};

export const setApplicationInformationIsLoaded = ({ commit }, payload) => {
    commit("setApplicationInformationIsLoaded", payload);
};

export const setPrivilegeNames = ({ commit }, payload) => {
    commit("setPrivilegeNames", payload);
};

export const setContainerLoader = ({ commit }, payload) => {
    commit("setContainerLoader", payload);
};
