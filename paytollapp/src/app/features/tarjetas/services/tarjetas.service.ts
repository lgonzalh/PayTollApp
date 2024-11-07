import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ITarjeta } from 'src/app/models/tarjeta.model';

@Injectable({
  providedIn: 'root'
})
export class TarjetasService {

  private baseUrl = 'http://localhost:5203/api/tarjetas'; // Servicio de tarjetas

  constructor(private http: HttpClient) { }

  // Método para obtener las tarjetas del usuario
  getTarjetas(): Observable<ITarjeta[]> {
    return this.http.get<ITarjeta[]>(`${this.baseUrl}`);
  }

  // Método para crear una nueva tarjeta
  addTarjeta(tarjeta: ITarjeta): Observable<ITarjeta> {
    return this.http.post<ITarjeta>(`${this.baseUrl}`, tarjeta);
  }
}
