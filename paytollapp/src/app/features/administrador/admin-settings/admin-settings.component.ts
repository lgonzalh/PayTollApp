import { Component } from '@angular/core';

@Component({
  standalone: true, // Este componente es ahora standalone
  selector: 'app-admin-settings',
  templateUrl: './admin-settings.component.html',
  styleUrls: ['./admin-settings.component.css'],
  imports: [] // Esto es válido solo en standalone
})
export class AdminSettingsComponent { }
