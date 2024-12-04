import { Component, OnInit } from '@angular/core';
import { AssuntoService } from 'src/app/services/assunto.service';

@Component({
  selector: 'app-assunto',
  templateUrl: './assunto.component.html',
  styleUrls: ['./assunto.component.css']
})
export class AssuntoComponent implements OnInit {
  assuntos: any[] = [];

  constructor(private assuntoService: AssuntoService) {}

  ngOnInit(): void {
    this.loadAssuntos();
  }

  loadAssuntos(): void {
    this.assuntoService.getAll().subscribe(data => {
      debugger
      this.assuntos = data.content;
    });
  }

  newAssunto(): void {
    // Lógica para criar novo assunto
  }

  editAssunto(assunto: any): void {
    // Lógica para editar assunto
  }

  deleteAssunto(id: number): void {
    this.assuntoService.delete(id).subscribe(() => {
      this.loadAssuntos();
    });
  }
}
