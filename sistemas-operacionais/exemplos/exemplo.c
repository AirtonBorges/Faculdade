#include <stdio.h>
#include <unistd.h>
#include <time.h>


int main( int argc, char *argv[]){
	pthread_t t1, t2, t3, t4;
	time1 = (double) clock();
	time1 = time1 / CLOCKS_PER_SEC;
	int count = 0;
	while(count < 1500000000) {
		count++;
	}
	timedif = ( ((double) clock()) / CLOCKS_PER_SEC) - time1;
	printf("The elapsed time is %f seconds\n", timedif);
}