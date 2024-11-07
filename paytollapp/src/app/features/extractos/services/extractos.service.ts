import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExtractosService {
  private apiUrl = 'http://localhost:5206/api/extracto'; // URL real de la API de extractos

  constructor(private http: HttpClient) { }

  // Método para obtener el listado de extractos
  getExtractos(): Observable<any> {
    return this.http.get(`${this.apiUrl}`);
  }

  // Puedes agregar métodos adicionales si es necesario
}
