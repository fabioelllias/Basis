CREATE TABLE public."Livro_Autor" (
	"Livro_Codl" int4 NOT NULL,
	"Autor_CodAu" int4 NOT NULL,
	CONSTRAINT "PK_Livro_Autor" PRIMARY KEY ("Livro_Codl", "Autor_CodAu")
);
CREATE INDEX "IX_Livro_Autor_Autor_CodAu" ON public."Livro_Autor" USING btree ("Autor_CodAu");


-- public."Livro_Autor" chaves estrangeiras

ALTER TABLE public."Livro_Autor" ADD CONSTRAINT "FK_Livro_Autor_Autor_Autor_CodAu" FOREIGN KEY ("Autor_CodAu") REFERENCES public."Autor"("CodAu") ON DELETE CASCADE;
ALTER TABLE public."Livro_Autor" ADD CONSTRAINT "FK_Livro_Autor_Livro_Livro_Codl" FOREIGN KEY ("Livro_Codl") REFERENCES public."Livro"("Codl") ON DELETE CASCADE;