import { Routes } from '@angular/router';
import { Login } from './auth/login/login';
import { Register } from './auth/register/register';
import { Dashboard } from './dashboard/dashboard';
import { ProductList } from './products/product-list/product-list';
import { Home } from './home/home';
import { CategoryList } from './categories/category-list/category-list';
import { SupplierList } from './suppliers/supplier-list/supplier-list';
import { StockList } from './stock/stock-list/stock-list';
import { OrderList } from './orders/order-list/order-list';

export const routes: Routes = [

    { path: '', component: Home },
    { path: '', redirectTo: 'login', pathMatch: 'full' },

    { path: 'login', component: Login },
    { path: 'register', component: Register },
    {
        path: 'dashboard',
        component: Dashboard,
        children: [
            { path: 'categories', component: CategoryList},
            { path: 'suppliers', component: SupplierList },
            { path: 'products', component: ProductList},
            { path: 'stock', component: StockList},
            { path: 'orders', component: OrderList} 
        ]
    }
];