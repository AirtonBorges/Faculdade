## Aula: [[Organizar/Aula 19-09-2023 Escalonamento]]
## Exercício
- Fazer exercício de escalonamento na IDE do Arduino

##### TODO:
1. Investigue a função das outras funções do FreeRTOS usadas no código Mutex_Arduino.
2. Como exercício para escalonador, faça: a. [[Altere esses parâmetros e verifique se há diferença na simulação]]. Teste as 4 possibilidade de configuração: i Notou diferença em alguma delas?
3. b. Agora altere a prioridade das tasks Task1 e Task2 para 2 e 3 e novamente testes as quatros possibilidade de configuração. Notou diferença em alguma delas? 
4. c. Agora faça o seguinte:
	1. i. Duplique a função TaskMutex e chame a nova de TaskMutexModificada.
	2. ii. Dentro da TaskMutexModificada, adicione uma variável chamada sleep (pode colocar outro nome) e incremente a mesma dentro do laço de repetição for(;;). Coloque a função vTaskDelay dentro de um if que verifica se sleep é menor que 2.
	3. iii. Agora teste novamente as 4 possibilidades de configuração de escalonamento. Consegue ver alguma diferença de comportamento?
	4. d. Faça uma pesquisa para dizer qual é a diferença entre escalonamento preemptivo e escalonamento cooperativo.

#### Recursos:
- https://private-zinc-3e1.notion.site/Aula-7-Escalonamento-31736ced893046228a857375d1f9dfab
- https://file.notion.so/f/f/a655f02d-9668-4fbc-8786-22c9b8fbd5b4/de68f92c-0152-42f2-a90e-fa5628972058/palestra-desafios-e-tecnicas-para-utilizar-o-linux-em-sistemas-de-tempo-real.pdf?id=45d7fb44-2083-4df0-aa58-5d330f169408&table=block&spaceId=a655f02d-9668-4fbc-8786-22c9b8fbd5b4&expirationTimestamp=1696183200000&signature=ltQA6a2qYa6AwbL3uYYtuPOUe2IxwKMlvBVtlaJClYk&downloadName=palestra-desafios-e-tecnicas-para-utilizar-o-linux-em-sistemas-de-tempo-real.pdf