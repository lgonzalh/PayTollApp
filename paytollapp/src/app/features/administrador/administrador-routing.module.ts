import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AdminSettingsComponent } from './admin-settings/admin-settings.component';

const routes: Routes = [
  { path: 'dashboard', component: AdminDashboardComponent }, // Standalone
  { path: 'settings', component: AdminSettingsComponent }   // Standalone
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministradorRoutingModule { }
