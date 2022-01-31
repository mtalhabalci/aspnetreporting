import LayoutBlank from '@/layouts/LayoutBlank';

export default [{
    path: '/error',
    component: LayoutBlank,
    children: [
        {
            name: 'error-401',
            path: '401',
            component: () => import('@/views/Error/401')
        },
        {
            name: 'error-404',
            path: '404',
            component: () => import('@/views/Error/404')
        }
    ]
}];
