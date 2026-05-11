import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ItemDto {
  id: number;
  nome: string;
  descricao: string | null;
  caixaId: number;
}

export interface CaixaDto {
  id: number;
  numero: number;
  descricao: string | null;
  itens: ItemDto[];
}

export interface CriarCaixaRequest {
  numero: number;
  descricao: string | null;
}

export interface AdicionarItemRequest {
  nome: string;
  descricao: string | null;
  caixaId: number;
}

export interface BuscarItemRequest {
  termoBusca: string;
}

export interface BuscarItemResponse {
  encontrado: boolean;
  mensagem: string | null;
  caixaId: number | null;
  numeroCaixa: number | null;
}

@Injectable({
  providedIn: 'root'
})
export class MovingService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:5000/api';

  getCaixas(): Observable<CaixaDto[]> {
    return this.http.get<CaixaDto[]>(`${this.baseUrl}/caixas`);
  }

  getCaixaById(id: number): Observable<CaixaDto> {
    return this.http.get<CaixaDto>(`${this.baseUrl}/caixas/${id}`);
  }

  criarCaixa(request: CriarCaixaRequest): Observable<CaixaDto> {
    return this.http.post<CaixaDto>(`${this.baseUrl}/caixas`, request);
  }

  adicionarItem(request: AdicionarItemRequest): Observable<ItemDto> {
    return this.http.post<ItemDto>(`${this.baseUrl}/itens`, request);
  }

  buscarItem(request: BuscarItemRequest): Observable<{ respostaIA: string } | BuscarItemResponse> {
    return this.http.post<{ respostaIA: string } | BuscarItemResponse>(`${this.baseUrl}/buscar`, request);
  }
}