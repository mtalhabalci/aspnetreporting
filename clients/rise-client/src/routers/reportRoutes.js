import LayoutDefault from '@/layouts/Default';
export default [{
    path: '/report',
    component: LayoutDefault,
    children: [
        {
            name: 'report-list',
            path: 'list',
            component: () => import('@/views/Report/List'),
            meta: {
            }
        }
    ]
}];