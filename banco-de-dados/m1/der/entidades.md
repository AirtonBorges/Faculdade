- Escola (Id, Nome)
- Curso (Id, Nome, Tipo)
- Disciplina (Id, IdProfessor, Ementa, NumeroCreditos, NumeroMaximoAlunos)
- DisciplinaEmCursoOuEscola(Id, IdCurso, IdEscola, IdDisciplina, EhPrerequisito)

- Pessoa (Id, Nome, IdEndereco)
- Endereco (Id, Logradouro, Número, Complemento, Bairro, Cidade, Estado, Cep)
- ContatosEmPessoa (Id, IdPessoa, Tipo, Contato)

- Aluno (Id, IdPessoa, IdCurso, IdProfessorOrientador, IngressoDataHora, Status, Formacao, LinhaPesquisa)

- AlunoEmDisciplina (Id, IdAluno, IdDisciplinaEmCursoOuEscola, IdNotasAlunoEmDisciplina, FrequenciaPercentual)
- NotasAlunoEmDisciplina (Id, Nota1M1, Nota2M1, Nota1M2, Nota2M2, Nota1M3, Nota3M3)
- AlunoSemestreLetivo (Id, IdAluno, Nome, IdAlunoEmDisciplina1, IdAlunoEmDisciplina2, IdAlunoEmDisciplina3, IdAlunoEmDisciplina4, IdAlunoEmDisciplina5)

- Professor(Id, IdPessoa, IdEscola)