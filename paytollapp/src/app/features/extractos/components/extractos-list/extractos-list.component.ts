import { Component, OnInit } from '@angular/core';
import { ExtractosService } from '../../services/extractos.service';

@Component({
  selector: 'app-extractos-list',
  templateUrl: './extractos-list.component.html',
  styleUrls: ['./extractos-list.component.css']
})
export class ExtractosListComponent implements OnInit {
  extractos: any[] = [];

  constructor(private extractosService: ExtractosService) { }

  ngOnInit(): void {
    this.loadExtractos();
  }

  loadExtractos(): void {
    this.extractosService.getExtractos().subscribe({
      next: (data) => {
        this.extractos = data;
      },
      error: (err) => {
        console.error('Error loading extractos', err);
      }
    });
  }
}
