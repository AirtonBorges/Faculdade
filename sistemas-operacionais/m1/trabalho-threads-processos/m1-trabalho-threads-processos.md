## TODOS:
- [] Adicionar enunciado aqui

## Projeto 2
- [] Realize uma implementação em sua linguagem de preferência de algoritmo de ordenação de vetores
Bubble sort.
- [] O vetor deverá ser de pelo menos 200 posições e deverá ser comparado um sistema singlethread com um sistema multithread (com pelo menos 2 threads). - Além disso, a ordenação deverá
ser na ordem crescente (do maior para o menor) e o vetor de valores deve ser iniciado do maior para o
menor (descrente) não importando o valor das posições, desde que respeita essa regra. 
- [] Isso gerará o pior caso de uso do bubble sort. Exemplo do bubble sort e outros algoritmos

## Exemplos:
https://github.com/VielF/SO-Codes


## Problemas
- [] Calculo de matrizes não está mais rápido, por quê? 
#### Hipotese 1: estão iterando muitos itens
- [] Teste: Ver quanto tempo uma thread unica demora pra executar
Multiplicacao sem threads: 
The elapsed time is 0.587363 seconds
Multiplicacao com threads: 
The elapsed time is 0.079316 seconds <-- Uma thread

Multiplicacao sem threads: 
The elapsed time is 0.479807 seconds
Multiplicacao com threads: 
The elapsed time is 0.269583 seconds <-- Duas threads (parece sequencial)

- threads individuais tem virtualmente a mesma quantidade de tempo de execução