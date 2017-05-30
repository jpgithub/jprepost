#include <sched.h>
#include <errno.h>
#include <string.h>
#include <iostream>
#include <unistd.h>
#include <stdlib.h>
#include <fcntl.h>
#include <iostream>

#define myFIFO "/tmp/fifo_pipe"


using namespace std;

int GetCpuAffinity(pid_t pid,cpu_set_t cpuset,int cpu_num);
void SetCpuAffinity(pid_t pid,cpu_set_t cpuset,int cpu_num);

int main (int argc, char* argv[])
{
	if (argc < 2){
		cout<<"usage: processnumber [CPU_ID]"<<endl;
		return -1;
	}
	
	cpu_set_t cpu0,cpu1;
	pid_t myPid = getpid();
	cout<<"Process Id: "<<myPid<<endl;		
	int cpuid = atoi(argv[1]);
	
	cout<<"Before CPU Affinity: "<<endl;
	if(GetCpuAffinity(myPid,cpu0,cpuid) > 0){
		cout<<"After CPU Affinity: "<<endl;
		SetCpuAffinity(myPid,cpu0,cpuid);
		
		
		int fifo = open(myFIFO, O_RDONLY);
     	if (fifo  < 0) { 
     		return -1;
     	}

		while(true){
			char value[256];		
			int num = read(fifo, value, sizeof(value));

			if(num < 0) { 
		 		//printf("\n %s \n", strerror_s(errno));
		 		return -1;
		 	}else if ( num == 0){
		 		break;
		 	}
		 	
			unsigned long l =  atoi(value); //0xC0000000;
			cout << l << endl;
			while(l > 0lu){
				l--;
			}
		}
		return 0;
	}	
		
}

int GetCpuAffinity(pid_t pid, cpu_set_t cpuset, int cpu_num){
	int is_success = sched_getaffinity(pid,sizeof(cpuset),&cpuset);
	// If not success print out the error message saved in errno system global variable
	if(is_success != 0){
		cout<<strerror(errno)<<endl;
		return is_success;
	
	}else{
		int mask = 1;
		for (int i = 0; i < CPU_SETSIZE; i++)
		{
        	if (CPU_ISSET(i, &cpuset)){
            	cout<<"CPU"<<cpu_num<<": CPU "<<i<<endl;
				mask |= mask;
			}
		}
		return mask;
	}
}

void SetCpuAffinity(pid_t pid, cpu_set_t cpuset, int cpu_num){
		CPU_ZERO(&cpuset);
		CPU_SET(cpu_num,&cpuset);
		// Set CPU Affinity
		int is_success = sched_setaffinity(pid,sizeof(cpuset),&cpuset);
				
		if(is_success != 0){
			cout<<strerror(errno)<<endl;
		}
		
		for (int i = 0; i < CPU_SETSIZE; i++)
		{
        	if (CPU_ISSET(i, &cpuset)){
            			cout<<"CPU" << cpu_num <<": CPU "<<i<<endl;
			}
		}
}		