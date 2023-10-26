## Normalização do trabalho da M1
Contato (Id, Tipo, Contato)
IdContatoEmPessoa (Id, IdPesoa, IdEndereco)
Endereco (Id, Logradouro, Numero, Complemento, Bairro, Cidade, Estado, CEP)
IdEndereco (Id, IdPessoa, IdEndereco)
EnderecoEmPessoa (Id, IdPesoa, IdEndereco)
Pessoa (Id, Nome, Email)

Aluno(Id, IdPessoa, IdProfessorOrientador, IdCurso, DataIngresso, Status, Formacao, LinhaPesquisa)
DisciplinaEmAluno(
    Id,
    IdAluno,
    IdDisciplina,
    SemestreLetivo,
    FrequenciaPercentual,
    Media1Nota1,
    Media1Nota2,
    Media2Nota1,
    Media2Nota2,
    Media3Nota1,
    Media3Nota2
)
Professor(Id, IdPessoa, IdEscola)

Curso (Id, IdEscola, Nome)

Disciplina(Id, IdCurso, IdEscola, IdProfessor, Ementa, Nome, NumeroCreditos, NumeroCreditos, NumeroMaximoAlunos)
Prerequisito(Id, IdDisciplina, IdDiscipliaPrerequisito)

Escola(Id, Nome)