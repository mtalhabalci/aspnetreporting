import LayoutDefault from '@/layouts/Default';
export default [{
    path: '/home',
    component: LayoutDefault,
    children: [
        {
            name: 'home-index',
            path: 'index',
            component: () => import('@/views/Index'),
            meta: {
            }
        }
    ]
}];