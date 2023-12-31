#include <pthread.h>
#include <stdio.h>
#include <unistd.h>
#include <time.h>
#include <math.h>

#define TAMANHO_VETOR 18000

int valor_total = 0;
int vetor[TAMANHO_VETOR];
double timer = 0;

void inicia_timer() {
	timer = (double) clock();
	timer = timer / CLOCKS_PER_SEC;
}

double finaliza_timer() {
	double timedif = ( ((double) clock()) / CLOCKS_PER_SEC) - timer;
    return timedif;
}

void preenche_vetor() {
    for (int i = 0; i < TAMANHO_VETOR; i++) {
        vetor[i] = TAMANHO_VETOR - i;
    }
}

void mostra_vetor(void) {
    printf("\n[ ");
    for (int i = 0; i < TAMANHO_VETOR; i++) {
        printf("%d ", vetor[i]);
    }
    printf("]\n");
}

void ordena_vetor(int inicio, int fim, int* vetor) {
    int temporario = 0;
    for(int i = inicio; i < fim; i++)
    {
        for(int j = inicio; j < fim - 1; j++)
        {
            int atual = vetor[j];
            int proximo = vetor[j + 1];
            if (atual > proximo) {
                temporario = atual;
                vetor[j] = proximo;
                vetor[j + 1] = temporario;
            }
        }
    }
}

void ordena_thread_1(void *param) {
    int* vetor_inicial = param;
    int primeira_metade = TAMANHO_VETOR - (TAMANHO_VETOR / 2);
    ordena_vetor(0, primeira_metade, vetor_inicial);
}

void ordena_thread_2(void *param) {
    int* vetor_inicial = param;
    int primeira_metade = TAMANHO_VETOR - (TAMANHO_VETOR / 2);
    ordena_vetor(primeira_metade, TAMANHO_VETOR, vetor_inicial);
}

void ordena_com_threads() {
    pthread_t tid_ordena_vetor_1, tid_ordena_vetor_2;
    pthread_create(&tid_ordena_vetor_1, NULL, (void *) ordena_thread_1, vetor);
    pthread_create(&tid_ordena_vetor_2, NULL, (void *) ordena_thread_2, vetor);
    pthread_join(tid_ordena_vetor_2, NULL);
    pthread_join(tid_ordena_vetor_1, NULL);
}

int main(int argc, char *argv[]){
    double tempo_maximo_de_execucao_sem_threads = 0;
    double tempo_maximo_de_execucao_com_threads = 0;
    double tempo_sem_threads = 0;
    double tempo_com_threads = 0;
    
    for (int i = 0; i < 5; i++) {
        preenche_vetor();
        inicia_timer();
        ordena_vetor(0, TAMANHO_VETOR, vetor);
        tempo_sem_threads = finaliza_timer();
        if(tempo_sem_threads > tempo_maximo_de_execucao_sem_threads)
            tempo_maximo_de_execucao_sem_threads = tempo_sem_threads;


        preenche_vetor();
        inicia_timer();
        ordena_com_threads();
        tempo_com_threads = finaliza_timer();
        if(tempo_com_threads > tempo_maximo_de_execucao_com_threads)
            tempo_maximo_de_execucao_com_threads = tempo_com_threads;
    }

    printf("Ordenacao sem threads: \n");
    printf("The elapsed time is %f seconds\n", tempo_maximo_de_execucao_sem_threads);

    printf("Ordenacao com threads: \n");
    printf("The elapsed time is %f seconds\n", tempo_maximo_de_execucao_com_threads);
}