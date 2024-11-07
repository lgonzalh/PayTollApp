import { Component } from '@angular/core';

@Component({
  standalone: true, // Este componente es ahora standalone
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
  imports: [] // Esto es válido solo en standalone
})
export class AdminDashboardComponent { }
