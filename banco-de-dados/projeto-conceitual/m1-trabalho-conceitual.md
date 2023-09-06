Você foi contratado para fazer o projeto conceitual (DER) do banco de dados de um 
sistema para uma universidade. A universidade deseja armazenar os seguintes dados:
• A universidade é dividida em escolas. Cada escola possui um ID e um nome.
• Uma escola possui vários cursos, e para cada curso de graduação ou de pós graduação, deve-se armazenar o ID e o nome.
• Cada curso possui várias disciplinas, devendo armazenar o seu ID, o nome, a 
ementa, o número de créditos, quantidade máxima de alunos e as disciplinas que 
são pré-requisitos para esta. 
o Uma disciplina pode pertencer a um curso ou a uma escola (disciplina 
comum para vários cursos).
o Considere também que uma disciplina pode ser ou não pré-requisito para 
várias disciplinas e que uma disciplina pode ter vários ou nenhum pré requisito.
• Para os alunos, a universidade deseja armazenar o ID, o nome, o e-mail, o endereço
completo (logradouro, número, complemento, bairro, cidade, estado, cep), 
telefones (celulares, comerciais e residenciais), data de ingresso na universidade, e 
qual o status da matrícula do aluno (cancelado, ativo, trancamento).
o O aluno deverá estar associado a um único curso (graduação ou pós graduação), e poderá fazer até, no máximo, cinco disciplinas por semestre
letivo.
o Para cada disciplina que o aluno cursar, armazenar as duas notas para cada 
média (M1, M2 e M3), o semestre letivo (por exemplo, “2023-1”) e a 
frequência obtida (percentual).
o Para os alunos de pós-graduação, deseja-se saber a sua formação escolar,
a sua linha de pesquisa e o ID do seu professor orientador.
• Para os professores, a universidade deseja armazenar o ID, o nome, o e-mail, o 
endereço completo (logradouro, número, complemento, bairro, cidade, estado, 
cep) e telefones (celulares, comerciais e residenciais).
o Cada professor poderá ministrar em várias disciplinas, entretanto, uma 
disciplina poderá ter apenas um professor.
o Um professor está associado a apenas uma escola (escola que leciona o 
maior número de disciplinas).
o Um professor pode orientar vários alunos de pós-graduação, e o aluno de 
pós-graduação terá apenas um orientador.
Página 2
Considerações: todas as tabelas deverão possuir a coluna ID (chave primária). A 
universidade possui apenas um único campus.
Critérios de avaliação:
• Correta confecção do projeto conceitual de banco de dados;
• Correto uso das normas para a criação do diagrama solicitado (DER);
• Projeto em conformidade ao que foi solicitado no enunciado.
ATENÇÃO: Deverá ser postado no Material Didático (Intranet → Portal do Aluno) o que foi 
solicitado em uma única IMAGEM em boa resolução (PNG ou JPG) até às 23:59 do dia 
15/09