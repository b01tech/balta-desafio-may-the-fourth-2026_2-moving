import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovingService, CaixaDto } from '../../services/moving.service';

@Component({
  selector: 'app-caixa-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './caixa-list.html',
  styleUrl: './caixa-list.scss',
})
export class CaixaListComponent implements OnInit {
  private movingService = inject(MovingService);
  
  caixas = signal<CaixaDto[]>([]);
  loading = signal(true);
  error = signal<string | null>(null);
  expandedCaixas = signal<Set<number>>(new Set());

  ngOnInit() {
    this.loadCaixas();
  }

  loadCaixas() {
    this.loading.set(true);
    this.error.set(null);
    
    this.movingService.getCaixas().subscribe({
      next: (data) => {
        this.caixas.set(data);
        this.loading.set(false);
      },
      error: (err) => {
        this.error.set('Erro ao carregar caixas');
        this.loading.set(false);
      }
    });
  }

  toggleCaixa(id: number) {
    const current = this.expandedCaixas();
    const newSet = new Set(current);
    if (newSet.has(id)) {
      newSet.delete(id);
    } else {
      newSet.add(id);
    }
    this.expandedCaixas.set(newSet);
  }

  isExpanded(id: number): boolean {
    return this.expandedCaixas().has(id);
  }
}