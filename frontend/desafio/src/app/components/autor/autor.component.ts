import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AutorService } from 'src/app/services/autor.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-autor',
  templateUrl: './autor.component.html',
  styleUrls: ['./autor.component.css']
})
export class AutorComponent implements OnInit {
  @ViewChild('nomeInput') nomeInput!: ElementRef;
  autores: any[] = [];
  autorForm: FormGroup;
  isFormVisible = false;
  isEditMode = false;
  selectedAutorId: number | null = null;
  successMessage: string | null = null;

  constructor(private fb: FormBuilder, private autorService: AutorService) {
    this.autorForm = this.fb.group({
      nome: ['', [Validators.required, Validators.maxLength(40)]]
    });
  }

  ngOnInit(): void {
    this.loadAutors();
  }

  loadAutors(): void {
    this.autorService.getAll().subscribe(data => {
      this.autores = data.content;
    });
  }

  openForm(): void {
    this.isFormVisible = true;
    this.isEditMode = false;
    this.autorForm.reset();

    setTimeout(() => {
      this.nomeInput.nativeElement.focus();
    }, 0);
  }

  closeForm(): void {
    this.isFormVisible = false;
    this.autorForm.reset();
    this.selectedAutorId = null;
  }

  onSubmit(): void {
    if (this.autorForm.invalid) {
      return;
    }

    const autorData = this.autorForm.value;

    if (this.isEditMode && this.selectedAutorId) {
      this.autorService.update(this.selectedAutorId, autorData).subscribe(() => {
        this.loadAutors();
        this.displaySuccessMessage('Autor atualizado com sucesso!');
        this.closeForm();
      });
    } else {
      this.autorService.create(autorData).subscribe(() => {
        this.loadAutors();
        this.displaySuccessMessage('Autor criado com sucesso!');
        this.closeForm();
      });
    }
  }

  editAutor(autor: any): void {
    this.isFormVisible = true;
    this.isEditMode = true;
    this.selectedAutorId = autor.id;
    this.autorForm.patchValue({
      nome: autor.nome
    });

    setTimeout(() => {
      this.nomeInput.nativeElement.focus();
    }, 0);
  }

  deleteAutor(id: number): void {
    Swal.fire({
      title: 'Excluir registro?',
      text: 'Esta ação não pode ser desfeita!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sim',
      cancelButtonText: 'Não'
    }).then((result) => {
      if (result.isConfirmed) {
        this.autorService.delete(id).subscribe(() => {
          this.loadAutors();
          Swal.fire('Excluído!', 'Autor excluído com sucesso!', 'success');
        });
      }
    });
  }

  displaySuccessMessage(message: string): void {
    this.successMessage = message;
    setTimeout(() => {
      this.successMessage = null;
    }, 3000);
  }
}
