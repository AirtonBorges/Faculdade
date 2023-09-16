#include	<pthread.h>
#include	<stdio.h>
#include	<unistd.h>

#define TAMANHO_VETOR 1000000

int valor_total = 0;
int vetor[TAMANHO_VETOR];
void preenche_vetor(void) {
    int valor = TAMANHO_VETOR;
    for (int i = 0; i <= TAMANHO_VETOR; i++) {
        vetor[i] = valor - i;
    }
}

void mostra_vetor(void) {
    printf("\n[ ");
    for (int i = 0; i <= TAMANHO_VETOR; i++) {
        printf("%d ", vetor[i]);
    }
    printf("]\n");
}

void ordena_vetor(int vetor[], int tamanho) {
    int index = 0;
    int index2 = 0;
    int temporario = 0;
    while (1) {
        while (1) {
            int atual = vetor[index];
            int proximo = vetor[index + 1];
            if (atual > proximo) {
                temporario = atual;
                vetor[index] = proximo;
                vetor[index + 1] = temporario;
            }
            index++;
            if (index == tamanho) {
                index = 0;
                break;
            }
        }
        index2++;
        if (index2 == tamanho) {
            break;
        }
    }
}

int main( int argc, char *argv[]){
    pthread_t tid;
    pthread_attr_t attr; 

    pthread_attr_init(&attr);
    pthread_create(&tid,&attr,runner,argv[1]);

    preenche_vetor();
    mostra_vetor();
    ordena_vetor(vetor, TAMANHO_VETOR / 2);
    ordena_vetor(vetor, TAMANHO_VETOR / 2);
    mostra_vetor();

    // pthread_create(&t1, NULL, (void *) thread_ordena, NULL);
    // pthread_join(tid,NULL);
}