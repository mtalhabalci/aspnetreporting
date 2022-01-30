export default {
    console: {
        developmentLog: function (log) {
            if (process.env.NODE_ENV === 'development' || process.env.NODE_ENV === 'test') {
                console.log(log);
            }
        }
    },
    project: {
        env: process.env.NODE_ENV,
        version: process.env.VUE_APP_VERSION || '0',
        name: process.env.VUE_APP_PROJECT_NAME || 'VUE_APP_PROJECT_NAME',
        pageTitle: process.env.VUE_APP_PROJECT_NAME || 'VUE_APP_PROJECT_NAME',
        company: process.env.VUE_APP_PROJECT_COMPANY || 'VUE_APP_PROJECT_COMPANY'
    },

    page: {
        showDescription: process.env.VUE_APP_PAGE_SHOW_DESCRIPTION || true,
        showDescriptionInTooltip: process.env.VUE_APP_PAGE_SHOW_DESCRIPTION_TOOLTIP || false
    },

    settings: {
        dateFormat: 'DD/MM/YYYY',
        dateTimeFormat: 'DD/MM/YYYY HH:mm:ss'
    }
};
