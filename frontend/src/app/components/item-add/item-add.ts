import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MovingService, CaixaDto } from '../../services/moving.service';

@Component({
  selector: 'app-item-add',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './item-add.html',
  styleUrl: './item-add.scss',
})
export class ItemAddComponent {
  private movingService = inject(MovingService);
  
  nome = signal('');
  descricao = signal('');
  caixaId = signal<number | null>(null);
  caixas = signal<CaixaDto[]>([]);
  loading = signal(false);
  success = signal<string | null>(null);
  error = signal<string | null>(null);

  constructor() {
    this.loadCaixas();
  }

  loadCaixas() {
    this.movingService.getCaixas().subscribe({
      next: (data) => {
        this.caixas.set(data);
        if (data.length > 0) {
          this.caixaId.set(data[0].id);
        }
      }
    });
  }

  adicionar() {
    if (!this.nome() || !this.caixaId()) {
      this.error.set('Nome e caixa são obrigatórios');
      return;
    }

    this.loading.set(true);
    this.error.set(null);
    this.success.set(null);

    this.movingService.adicionarItem({
      nome: this.nome(),
      descricao: this.descricao() || null,
      caixaId: this.caixaId()!
    }).subscribe({
      next: () => {
        this.success.set('Item adicionado com sucesso!');
        this.nome.set('');
        this.descricao.set('');
        this.loading.set(false);
        setTimeout(() => this.success.set(null), 3000);
      },
      error: (err) => {
        this.error.set('Erro ao adicionar item');
        this.loading.set(false);
      }
    });
  }
}