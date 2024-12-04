CREATE VIEW vw_autor_livro_assunto AS
SELECT
    a."Nome" AS autor,
    l."Titulo" AS livro,
    l."Editora" AS editora,
    l."AnoPublicacao" AS anopublicacao,
    STRING_AGG(s."Descricao", ', ') AS assuntos
FROM
    "Autor" a
join "Livro_Autor" la ON a."CodAu" = la."Autor_CodAu"
join "Livro" l ON la."Livro_Codl" = l."Codl"
join "Livro_Assunto" ls ON l."Codl" = ls."Livro_Codl"
join "Assunto" s ON ls."Assunto_codAs" = s."codAs"
group by a."Nome", l."Titulo", l."Editora", l."AnoPublicacao";
