#include <stdio.h> 
#include <iostream>
#include <fcntl.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <unistd.h>
#include <errno.h>

using namespace std;
#define myFIFO "/tmp/fifo_pipe"

int main(){
    int num, fifo, status;
    char temp[32];

	status = mkfifo(myFIFO, 0666);
    if (status < 0) { 
     printf("\n %s \n", "Problem");
     return 0;
     }

	fifo = open(myFIFO, O_RDONLY);
     if (fifo  < 0) { 
     //printf("\n %s \n", strerror_s(errno));
     return 0;
     }

	// int tot = 0;
	//while (tot < sizeof(temp)){
	// num = read(fifo, temp + tot, sizeof(temp) - tot);
	// if (num < 0)
	// tot += num;
	// }
     num = read(fifo, temp, sizeof(temp));
	if(num < 0) { 
     //printf("\n %s \n", strerror_s(errno));
     return 0;
     }

    printf("In FIFO is %s \n", temp);
}