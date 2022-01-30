import 'vue-sidebar-menu/dist/vue-sidebar-menu.css'

import DefaultModuleRoutes from '@/routers/homeRoutes';
import ErrorRoutes from '@/routers/errorRoutes';
import ReportRoutes from '@/routers/reportRoutes';
import PersonRoutes from '@/routers/personRoutes';
import Router from 'vue-router';
import Vue from 'vue';
import VueSidebarMenu from 'vue-sidebar-menu'

import middlewarePipeline from '@/middlewares/middlewarePipeline';
import store from '@/store/store';

Vue.use(VueSidebarMenu)
// import LayoutDefault from '@/layouts/Default';

Vue.use(Router);


const ROUTES = [
    { path: '/', redirect: '/home/index' }, // Default route
    { path: '*', redirect: '/error/404' }// 404
]
    .concat(DefaultModuleRoutes)
    .concat(ErrorRoutes).concat(ReportRoutes)
.concat(PersonRoutes)

    ;

if (!window.localStorage.lang)
    window.localStorage.setItem("lang", "tr");

const lang = window.localStorage.lang;

//if url which comes from browser without route
var browserPath = window.location;
var urlIncludeLang = "/" + lang + "/";
if (!browserPath.href.includes(urlIncludeLang) && browserPath.pathname != "/") {
    var addLang = browserPath.origin + "/" + lang + browserPath.pathname;
    browserPath.href = addLang;
}

const router = new Router({
    base: lang,
    mode: 'history',
    routes: ROUTES,
});

router.afterEach(() => {
    const splashScreen = document.querySelector('.app-splash-screen');
    if (splashScreen) {
        setTimeout(() => {
            splashScreen.style.opacity = 0;
            splashScreen.parentNode.removeChild(splashScreen);
        }, 2000);
    }

    if (window.layoutHelpers && window.layoutHelpers.isSmallScreen() && !window.layoutHelpers.isCollapsed()) {
        setTimeout(() => window.layoutHelpers.setCollapsed(true, true), 10);
    }
});

router.beforeEach((to, from, next) => {
    document.body.classList.add('app-loading');
    setTimeout(() => next(), 200);

    if (!to.meta.middleware) {
        return next();
    }
    const middleware = to.meta.middleware;

    const context = {
        to,
        from,
        next,
        router,
        store
    };
    return middleware[0] ? middleware[0]({
        ...context,
        next: middlewarePipeline(context, middleware, 1)
    }) : next();
});

export default router;
