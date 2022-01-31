import LayoutDefault from '@/layouts/Default';
export default [{
    path: '/person',
    component: LayoutDefault,
    children: [
        {
            name: 'person.create',
            path: 'create',
            component: () => import('@/views/Person/CreateOrEdit'),
            meta: {
            }
        },
        {
            name: 'person.edit',
            path: 'edit/:id',
            component: () => import('@/views/Person/CreateOrEdit'),
            meta: {
            }
        },
        {
            name: 'person.contact.information',
            path: 'contactinformation/:id',
            component: () => import('@/views/Person/ContactInformationList'),
            meta: {
            }
        },
    ]
}];