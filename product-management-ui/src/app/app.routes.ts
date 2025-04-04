import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'login',
        loadComponent: () => import('./modules/auth/login/login.component')
            .then(m => m.LoginComponent)
    },
    {
        path: 'register',
        loadComponent: () => import('./modules/auth/register/register.component')
            .then(m => m.RegisterComponent)
    },
    {
        path: '',
        loadComponent: () => import('./modules/components/layouts/layouts.component')
            .then(m => m.LayoutsComponent),
        children: [            
            {
                path: '',
                loadComponent: () => import('./modules/products/products.component')
                    .then(m => m.ProductsComponent)
            },                     
            
        ]
    }
];