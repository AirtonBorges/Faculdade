#include <pthread.h>
#include <stdio.h>
#include <time.h>
#include <stdlib.h> 

#define DIMENSAO_MATRIZ 25

double matriz_1[DIMENSAO_MATRIZ][DIMENSAO_MATRIZ];
double matriz_2[DIMENSAO_MATRIZ][DIMENSAO_MATRIZ];
double matriz_matricial[DIMENSAO_MATRIZ][DIMENSAO_MATRIZ];
double matriz_posicional[DIMENSAO_MATRIZ][DIMENSAO_MATRIZ];

double timer = 0;

void inicia_timer() {
    timer = 0;
	timer = (double) clock();
	timer = timer / CLOCKS_PER_SEC;
}

double finaliza_timer() {
	double timedif = ( ((double) clock()) / CLOCKS_PER_SEC) - timer;
    return timedif;
}

void preenche_matrizes() {
    for (int i = 0; i < DIMENSAO_MATRIZ; i++) {
        for (int j = 0; j < DIMENSAO_MATRIZ; j++) {
            matriz_1[i][j] = rand() % 10;
            matriz_2[i][j] = rand() % 10;
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

void multiplicacao_matriz(int coluna_inicio
    , int coluna_fim
    , double pMatriz_1[DIMENSAO_MATRIZ][DIMENSAO_MATRIZ]
    , double pMatriz_2[DIMENSAO_MATRIZ][DIMENSAO_MATRIZ])
{
    for (int i = 0; i < DIMENSAO_MATRIZ; i++) {
        for (int j = coluna_inicio; j < coluna_fim; j++) {
            matriz_matricial[i][j] = pMatriz_1[i][j] * pMatriz_2[j][i];
            mostra_matriz(matriz_matricial);
        }
    }
    for (int i = 0; i < DIMENSAO_MATRIZ; i++) {
        for (int j = coluna_inicio; j < coluna_fim; j++) {
            matriz_posicional[i][j] = pMatriz_1[i][j] * pMatriz_2[i][j];
            mostra_matriz(matriz_posicional);
        }
    }
}

void multiplica_matriz_1() {
    int primeiro_quarto = 0 - DIMENSAO_MATRIZ - (DIMENSAO_MATRIZ / 2);
    multiplicacao_matriz(0, primeiro_quarto, matriz_1, matriz_2);
}

void multiplica_matriz_2() {
    int primeiro_quarto = DIMENSAO_MATRIZ - 3*(DIMENSAO_MATRIZ / 4);
    multiplicacao_matriz(primeiro_quarto, DIMENSAO_MATRIZ, matriz_1, matriz_2);
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
    double tempo_maximo_de_execucao_sem_threads = 0;
    double tempo_maximo_de_execucao_com_threads = 0;
    double tempo_sem_threads = 0;
    double tempo_com_threads = 0;
    
    for (int i = 0; i < 5; i++) {
        preenche_matrizes();
        inicia_timer();
        multiplicacao_matriz(0, DIMENSAO_MATRIZ, matriz_1, matriz_2);
        tempo_sem_threads = finaliza_timer();
        if(tempo_sem_threads > tempo_maximo_de_execucao_sem_threads)
            tempo_maximo_de_execucao_sem_threads = tempo_sem_threads;
 
        preenche_matrizes();
        inicia_timer();
        multiplicacao_matriz_com_threads();
        tempo_com_threads = finaliza_timer();
        if(tempo_com_threads > tempo_maximo_de_execucao_com_threads)
            tempo_maximo_de_execucao_com_threads = tempo_com_threads;

    }
    
    printf("Multiplicacao sem threads: \n");
    printf("The elapsed time is %f seconds\n", tempo_maximo_de_execucao_sem_threads);

    printf("Multiplicacao com threads: \n");
    printf("The elapsed time is %f seconds\n", tempo_maximo_de_execucao_com_threads);
}