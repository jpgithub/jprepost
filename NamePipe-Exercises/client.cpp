#include<stdio.h> 
#include<fcntl.h>
#include<stdlib.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <unistd.h>
#include <string.h>


using namespace std;
#define myFIFO "/tmp/fifo_pipe"

int main() {
    int status, num, fifo;
    char string[]="Testing...";

	fifo = open(myFIFO, O_WRONLY);
     if (fifo < 0) { 
     //printf("\n %s \n", stderr(errno));
     return 0;
     }

	num= write(fifo, string, strlen(string));
     if (num < 0) { 
     //printf("\n %s \n", strerror_s(errno));
     return 0;
     }
}