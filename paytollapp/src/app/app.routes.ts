import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'administrador',
    loadChildren: () => import('./features/administrador/administrador.module').then(m => m.AdministradorModule)
  },
  { path: '', redirectTo: '/administrador', pathMatch: 'full' } // Ruta por defecto
];
