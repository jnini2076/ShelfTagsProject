import { ProductsComponent } from './products/products.component';
import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { AdminComponent } from './admin/admin.component';

export const routes: Routes = [
  {path: '', component:HomeComponent},
  {path:"admin", component:AdminComponent},
  {path:"products",component:ProductsComponent}
];
