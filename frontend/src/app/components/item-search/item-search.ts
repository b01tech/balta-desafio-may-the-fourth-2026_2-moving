import { Component, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MovingService } from '../../services/moving.service';

interface AISearchResult {
  encontrado?: boolean;
  caixa?: number;
  mensagem?: string;
}

@Component({
  selector: 'app-item-search',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './item-search.html',
  styleUrl: './item-search.scss',
})
export class ItemSearchComponent {
  private movingService = inject(MovingService);
  
  termoBusca = signal('');
  loading = signal(false);
  resultado = signal<string | null>(null);
  error = signal<string | null>(null);

  parsedResult = computed(() => {
    const resposta = this.resultado();
    if (!resposta) return null;
    
    try {
      const jsonMatch = resposta.match(/\{[\s\S]*\}/);
      if (jsonMatch) {
        return JSON.parse(jsonMatch[0]) as AISearchResult;
      }
    } catch {
      return null;
    }
    return null;
  });

  buscar() {
    if (!this.termoBusca().trim()) {
      this.error.set('Digite o nome do item que deseja buscar');
      return;
    }

    this.loading.set(true);
    this.error.set(null);
    this.resultado.set(null);

    this.movingService.buscarItem({ termoBusca: this.termoBusca() }).subscribe({
      next: (data: any) => {
        if ('respostaIA' in data) {
          this.resultado.set(data.respostaIA);
        } else {
          this.resultado.set(data.mensagem);
        }
        this.loading.set(false);
      },
      error: (err) => {
        this.error.set('Erro ao buscar item');
        this.loading.set(false);
      }
    });
  }
}