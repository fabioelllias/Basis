import { AutorService } from './../../services/autor.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { AssuntoService } from 'src/app/services/assunto.service';
import { LivroService } from 'src/app/services/livro.service';

@Component({
  selector: 'app-livro',
  templateUrl: './livro.component.html',
  styleUrls: ['./livro.component.css'],
})
export class LivroComponent implements OnInit {
  livros: any[] = [];
  autores: any[] = [];
  assuntos: any[] = [];
  livroForm: FormGroup;
  isFormVisible = false;
  isEditMode = false;
  selectedLivroId: number | null = null;
  successMessage: string | null = null;

  constructor(private fb: FormBuilder, private livroService: LivroService, private autorService: AutorService, private assuntoService: AssuntoService) {
    this.livroForm = this.fb.group({
      titulo: ['', [Validators.required, Validators.maxLength(40)]],
      editora: ['', [Validators.required, Validators.maxLength(40)]],
      edicao: ['', [Validators.required ]],
      anoPublicacao: ['', [Validators.required, Validators.maxLength(4)]],
      autores: this.fb.array([]),
      assuntos: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.loadLivros();
    this.loadAutores();
    this.loadAssuntos();
  }

  loadLivros(): void {
    this.livroService.getAll().subscribe(data => {
      this.livros = data.content;
    });
  }

  loadAutores(): void {
    this.autorService.getAll().subscribe(data => {
      this.autores = data.content;
    });
  }

  loadAssuntos(): void {
    this.assuntoService.getAll().subscribe(data => {
      this.assuntos = data.content;
    });

    // this.assuntos = [
    //   { id: 1, descricao: 'Assunto 1' },
    //   { id: 2, descricao: 'Assunto 2' },
    // ];
  }

  openForm(): void {
    this.isFormVisible = true;
    this.isEditMode = false;
    this.livroForm.reset();
    this.clearFormArrays();
  }

  closeForm(): void {
    this.isFormVisible = false;
    this.livroForm.reset();
    this.clearFormArrays();
    this.selectedLivroId = null;
  }

  onSubmit(): void {
    if (this.livroForm.invalid) {
      return;
    }

    const livroData = this.livroForm.value;

    if (this.isEditMode && this.selectedLivroId !== null) {
      this.livroService.update(this.selectedLivroId, livroData).subscribe(() => {
        this.loadLivros();
        this.displaySuccessMessage('Livro atualizado com sucesso!');
        this.closeForm();
      });
    } else {
      this.livroService.create(livroData).subscribe(() => {
        this.loadLivros();
        this.displaySuccessMessage('Livro criado com sucesso!');
        this.closeForm();
      });
    }
  }

  editLivro(livro: any): void {
    this.isFormVisible = true;
    this.isEditMode = true;
    this.selectedLivroId = livro.id;
    this.livroForm.patchValue({
      titulo: livro.titulo,
      editora: livro.editora,
      edicao: livro.edicao,
      anoPublicacao: livro.anoPublicacao,
    });

    this.clearFormArrays();

    livro.autores.forEach((autor: any) => {
      this.addAutor(autor.id);
    });

    livro.assuntos.forEach((assunto: any) => {
      this.addAssunto(assunto.id);
    });
  }

  deleteLivro(id: number): void {
    if (confirm('Tem certeza de que deseja excluir este livro?')) {
      this.livroService.delete(id).subscribe(() => {
        this.loadLivros();
        this.displaySuccessMessage('Livro excluído com sucesso!');
      });
    }
  }

  displaySuccessMessage(message: string): void {
    this.successMessage = message;
    setTimeout(() => {
      this.successMessage = null;
    }, 3000);
  }

  get autoresFormArray(): FormArray {
    return this.livroForm.get('autores') as FormArray;
  }

  addAutor(id?: number): void {
    this.autoresFormArray.push(this.fb.control(id || null, Validators.required));
  }

  removeAutor(index: number): void {
    this.autoresFormArray.removeAt(index);
  }

  // Métodos para gerenciar assuntos
  get assuntosFormArray(): FormArray {
    return this.livroForm.get('assuntos') as FormArray;
  }

  addAssunto(id?: number): void {
    this.assuntosFormArray.push(this.fb.control(id || null, Validators.required));
  }

  removeAssunto(index: number): void {
    this.assuntosFormArray.removeAt(index);
  }

  clearFormArrays(): void {
    this.autoresFormArray.clear();
    this.assuntosFormArray.clear();
  }

  getAutorFormControl(index: number): FormControl {
    return this.autoresFormArray.at(index) as FormControl;
  }

  getAssuntoFormControl(index: number): FormControl {
    return this.assuntosFormArray.at(index) as FormControl;
  }
}
