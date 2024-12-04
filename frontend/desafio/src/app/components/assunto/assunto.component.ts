import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AssuntoService } from 'src/app/services/assunto.service';

@Component({
  selector: 'app-assunto',
  templateUrl: './assunto.component.html',
  styleUrls: ['./assunto.component.css']
})
export class AssuntoComponent implements OnInit {
  @ViewChild('descricaoInput') descricaoInput!: ElementRef;
  assuntos: any[] = [];
  assuntoForm: FormGroup;
  isFormVisible = false;
  isEditMode = false;
  selectedAssuntoId: number | null = null;
  successMessage: string | null = null; // Mensagem de sucesso

  constructor(private fb: FormBuilder, private assuntoService: AssuntoService) {
    this.assuntoForm = this.fb.group({
      descricao: ['', [Validators.required, Validators.maxLength(20)]]
    });
  }

  ngOnInit(): void {
    this.loadAssuntos();
  }

  loadAssuntos(): void {
    this.assuntoService.getAll().subscribe(data => {
      this.assuntos = data.content;
    });
  }

  openForm(): void {
    this.isFormVisible = true;
    this.isEditMode = false;
    this.assuntoForm.reset();

    setTimeout(() => {
      this.descricaoInput.nativeElement.focus();
    }, 0);
  }

  closeForm(): void {
    this.isFormVisible = false;
    this.assuntoForm.reset();
    this.selectedAssuntoId = null;
  }

  onSubmit(): void {
    if (this.assuntoForm.invalid) {
      return;
    }

    const assuntoData = this.assuntoForm.value;

    if (this.isEditMode && this.selectedAssuntoId) {
      this.assuntoService.update(this.selectedAssuntoId, assuntoData).subscribe(() => {
        this.loadAssuntos();
        this.displaySuccessMessage('Assunto atualizado com sucesso!');
        this.closeForm();
      });
    } else {
      this.assuntoService.create(assuntoData).subscribe(() => {
        this.loadAssuntos();
        this.displaySuccessMessage('Assunto criado com sucesso!');
        this.closeForm();
      });
    }
  }

  editAssunto(assunto: any): void {
    this.isFormVisible = true;
    this.isEditMode = true;
    this.selectedAssuntoId = assunto.id;
    this.assuntoForm.patchValue({
      descricao: assunto.descricao
    });

    setTimeout(() => {
      this.descricaoInput.nativeElement.focus();
    }, 0);
  }

  deleteAssunto(id: number): void {
    this.assuntoService.delete(id).subscribe(() => {
      this.displaySuccessMessage('Assunto excluído com sucesso!');
      this.loadAssuntos();
    });
  }

  // Método para exibir a mensagem de sucesso
  displaySuccessMessage(message: string): void {
    this.successMessage = message;
    setTimeout(() => {
      this.successMessage = null; // Oculta a mensagem após 3 segundos
    }, 3000);
  }
}
