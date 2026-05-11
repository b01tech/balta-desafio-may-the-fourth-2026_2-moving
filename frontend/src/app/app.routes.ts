import { Routes } from '@angular/router';
import { CaixaListComponent } from './components/caixa-list/caixa-list';
import { ItemAddComponent } from './components/item-add/item-add';
import { ItemSearchComponent } from './components/item-search/item-search';

export const routes: Routes = [
  { path: '', redirectTo: '/caixas', pathMatch: 'full' },
  { path: 'caixas', component: CaixaListComponent },
  { path: 'adicionar', component: ItemAddComponent },
  { path: 'buscar', component: ItemSearchComponent },
];
