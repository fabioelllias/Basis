import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AssuntoService } from 'src/app/services/assunto.service';

@Component({
  selector: 'app-assunto',
  templateUrl: './assunto.component.html',
  styleUrls: ['./assunto.component.css']
})
export class AssuntoComponent implements OnInit {
  assuntos: any[] = [];
  assuntoForm: FormGroup;
  isFormVisible = false; // Controla a exibição do formulário
  isEditMode = false; // Indica se é modo de edição
  selectedAssuntoId: number | null = null;

  constructor(private fb: FormBuilder, private assuntoService: AssuntoService) {
    this.assuntoForm = this.fb.group({
      descricao: ['', [Validators.required, Validators.maxLength(20)]]
    });
  }

  ngOnInit(): void {
    this.loadAssuntos();
  }

  // Carregar a lista de assuntos
  loadAssuntos(): void {
    this.assuntoService.getAll().subscribe(data => {
      this.assuntos = data.content;
    });
  }

  // Mostrar o formulário para novo assunto
  openForm(): void {
    this.isFormVisible = true;
    this.isEditMode = false;
    this.assuntoForm.reset(); // Limpa o formulário
  }

  // Fechar o formulário
  closeForm(): void {
    this.isFormVisible = false;
    this.assuntoForm.reset();
    this.selectedAssuntoId = null;
  }

  // Enviar o formulário
  onSubmit(): void {
    if (this.assuntoForm.invalid) {
      return;
    }

    const assuntoData = this.assuntoForm.value;

    if (this.isEditMode && this.selectedAssuntoId !== null) {
      // Atualizar assunto
      this.assuntoService.update(this.selectedAssuntoId, assuntoData).subscribe(() => {
        this.loadAssuntos();
        this.closeForm();
      });
    } else {
      // Criar novo assunto
      this.assuntoService.create(assuntoData).subscribe(() => {
        this.loadAssuntos();
        this.closeForm();
      });
    }
  }

  // Editar um assunto
  editAssunto(assunto: any): void {
    this.isFormVisible = true;
    this.isEditMode = true;
    this.selectedAssuntoId = assunto.id;
    this.assuntoForm.patchValue({
      descricao: assunto.descricao
    });
  }

  // Excluir um assunto
  deleteAssunto(id: number): void {
    this.assuntoService.delete(id).subscribe(() => {
      this.loadAssuntos();
    });
  }
}
