#include <pthread.h>
#include <stdio.h>
#include <unistd.h>
#include <time.h>
#include <stdlib.h> 
#include <math.h>

#define DIMENSAO_MATRIZ 100000
#define MATRICIAL_1_POSICIONAL_0 1

double matriz_1[DIMENSAO_MATRIZ][DIMENSAO_MATRIZ];
double matriz_2[DIMENSAO_MATRIZ][DIMENSAO_MATRIZ];
double matriz_multiplicada[DIMENSAO_MATRIZ][DIMENSAO_MATRIZ];
double timer = 0;

void inicia_timer() {
    timer = 0;
	timer = (double) clock();
	timer = timer / CLOCKS_PER_SEC;
}

void finaliza_timer() {
	double timedif = ( ((double) clock()) / CLOCKS_PER_SEC) - timer;
	printf("The elapsed time is %f seconds\n", timedif);
}

void preenche_matrizes() {
    for (int i = 0; i < DIMENSAO_MATRIZ; i++) {
        for (int j = 0; j < DIMENSAO_MATRIZ; j++) {
            matriz_1[i][j] = rand();
            matriz_2[i][j] = rand();
        }
    }
}

void mostra_matriz(double matriz[DIMENSAO_MATRIZ][DIMENSAO_MATRIZ]) {
    for (int i = 0; i < DIMENSAO_MATRIZ; i++) {
        printf("\n[ ");
        for (int j = 0; j < DIMENSAO_MATRIZ; j++) {
            printf("%0.1f ", matriz[i][j]);
        }
        printf("]\n");
    }
}

void progresso(int inicio, int fim, int passo) {
    // int final = fim - inicio;
    // int atual = (int) floor((passo / final) * 100);
    // printf(" ", atual, final);
}

void multiplicacao_matriz(int coluna_inicio
    , int coluna_fim
    , double pMatriz_1[DIMENSAO_MATRIZ][DIMENSAO_MATRIZ]
    , double pMatriz_2[DIMENSAO_MATRIZ][DIMENSAO_MATRIZ])
{
    
    if (MATRICIAL_1_POSICIONAL_0 == 1)  {
        for (int i = 0; i < DIMENSAO_MATRIZ; i++) {
            for (int j = coluna_inicio; j < coluna_fim; j++) {
                matriz_multiplicada[i][j] = pMatriz_1[i][j] * pMatriz_2[j][i];
            }
            progresso(coluna_inicio, coluna_fim, i);
        }
    }
    if (MATRICIAL_1_POSICIONAL_0 == 0)  {
        for (int i = 0; i < DIMENSAO_MATRIZ; i++) {
            for (int j = coluna_inicio; j < coluna_fim; j++) {
                matriz_multiplicada[i][j] = pMatriz_1[i][j] * pMatriz_2[i][j];
            }
            progresso(coluna_inicio, coluna_fim, i);
        }
    }
}

void multiplica_matriz_1() {
    int primeiro_quarto = DIMENSAO_MATRIZ - 3*(DIMENSAO_MATRIZ / 4);
    multiplicacao_matriz(0, primeiro_quarto, matriz_1, matriz_2);
}

void multiplica_matriz_2() {
    int primeiro_quarto = DIMENSAO_MATRIZ - 3*(DIMENSAO_MATRIZ / 4);
    int segundo_quarto = DIMENSAO_MATRIZ - 2*(DIMENSAO_MATRIZ / 4);
    multiplicacao_matriz(primeiro_quarto, segundo_quarto, matriz_1, matriz_2);
}

void multiplicacao_matriz_com_threads() {
    pthread_t tid_multiplica_matriz_1, tid_multiplica_matriz_2;

    pthread_create(&tid_multiplica_matriz_1, NULL, (void *) multiplica_matriz_1, NULL);
    pthread_create(&tid_multiplica_matriz_2, NULL, (void *) multiplica_matriz_2, NULL);
    pthread_join(tid_multiplica_matriz_1, NULL);
    pthread_join(tid_multiplica_matriz_2, NULL);
}

int main(int argc, char *argv[]){
    // TODO: tempo de processamento médio
    printf("Multiplicacao sem threads: \n");
    preenche_matrizes();
    inicia_timer();
    multiplicacao_matriz(0, DIMENSAO_MATRIZ, matriz_1, matriz_2);
    finaliza_timer();

    preenche_matrizes();
    inicia_timer();
    printf("Multiplicacao com threads: \n");
    multiplicacao_matriz_com_threads();
    finaliza_timer();
}