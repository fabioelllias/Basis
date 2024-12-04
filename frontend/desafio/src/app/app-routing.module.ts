import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssuntoComponent } from './components/assunto/assunto.component';
import { AutorComponent } from './components/autor/autor.component';
import { LivroComponent } from './components/livro/livro.component';

const routes: Routes = [
  { path: 'assunto', component: AssuntoComponent },
  { path: 'autor', component: AutorComponent },
  { path: 'livro', component: LivroComponent },
  { path: '', redirectTo: '/livro', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
