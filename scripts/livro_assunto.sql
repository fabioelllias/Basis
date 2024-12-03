CREATE TABLE public."Livro_Assunto" (
	"Livro_Codl" int4 NOT NULL,
	"Assunto_codAs" int4 NOT NULL,
	CONSTRAINT "PK_Livro_Assunto" PRIMARY KEY ("Livro_Codl", "Assunto_codAs")
);
CREATE INDEX "IX_Livro_Assunto_Assunto_codAs" ON public."Livro_Assunto" USING btree ("Assunto_codAs");


-- public."Livro_Assunto" chaves estrangeiras

ALTER TABLE public."Livro_Assunto" ADD CONSTRAINT "FK_Livro_Assunto_Assunto_Assunto_codAs" FOREIGN KEY ("Assunto_codAs") REFERENCES public."Assunto"("codAs") ON DELETE CASCADE;
ALTER TABLE public."Livro_Assunto" ADD CONSTRAINT "FK_Livro_Assunto_Livro_Livro_Codl" FOREIGN KEY ("Livro_Codl") REFERENCES public."Livro"("Codl") ON DELETE CASCADE;